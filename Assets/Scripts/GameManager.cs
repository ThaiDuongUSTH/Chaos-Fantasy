using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // These are the possible states of the game while running
    public enum GameState
    {
        Playing,
        Paused,
        LevelUp
    }
    public GameState currentState;

    [Header("Stopwatch")]
    public TextMeshProUGUI stopWatchDisplay;
    private float stopWatchTime;

    public static GameManager instance;

    [Header("Screens")]
    public GameObject levelUpScreen;
    [Header("UI Elements")]
    public GameObject pauseMenu; 
    public Button pauseButton; 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // Prevent the creation of another instance of this class
        else
        {
            Debug.Log("GameManager duplicate destroyed: " + this);
            Destroy(gameObject);
        }

        DisableScreens();
    }

    void DisableScreens()
    {
        levelUpScreen.SetActive(false);
        pauseMenu.SetActive(false);
    }
    void Start()
    {
        currentState = GameState.Playing;
        pauseMenu.SetActive(false);  // Hide the pause menu initially

        // Set up the button's onClick event to toggle pause state
        pauseButton.onClick.AddListener(TogglePause);
    }
    void Update()
    {
        if (currentState == GameState.Playing)
        {
            UpdateStopWatch();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the game is paused then resume it
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            // If the game is playing then pause it
            else if (currentState == GameState.Playing)
            {
                TogglePause();
            }
        }

    }
    public void TogglePause()
    {
        if (currentState == GameState.Paused)
        {
            ResumeGame();
        }
        else if (currentState == GameState.Playing)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        currentState = GameState.Paused;
        // The scale at which time passes
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void StartLevelUpScreen()
    {
        currentState = GameState.LevelUp;
        Time.timeScale = 0f;
        levelUpScreen.SetActive(true);
    }

    public void EndLevelUpScreen()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
        levelUpScreen.SetActive(false);
    }

    void UpdateStopWatch()
    {
        stopWatchTime += Time.deltaTime;
        UpdateStopWatchDisplay();
    }

    void UpdateStopWatchDisplay()
    {
        int minutes = (int)Math.Floor(stopWatchTime / 60);
        int seconds = (int)Math.Floor(stopWatchTime % 60);

        stopWatchDisplay.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}
