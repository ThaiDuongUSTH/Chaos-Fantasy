using UnityEngine;

// Base weapon script
public class Weapon : Item
{
    public WeaponData weaponData;
    private float cooldown;
    private float currentCooldownDuration;

    protected PlayerMovement pm;

    public virtual void Initialize(WeaponData data)
    {
        base.Initialize(data);

        currentCooldownDuration = data.cooldownDuration * (1 - owner.currentCooldownReduction / 100f);
        Debug.Log("Init current cooldown: "+ currentCooldownDuration + ", weapon: " + weaponData.itemName);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        Initialize(weaponData);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            Attack();
        }
    }

    public void ApplyBuffs() 
    {
        currentCooldownDuration = weaponData.cooldownDuration * (1 - owner.currentCooldownReduction / 100f);
        Debug.Log("level up current cooldown: "+ currentCooldownDuration + ", weapon: " + weaponData.itemName);
    }

    public override void LevelUp()
    {
        base.LevelUp();
    }

    protected virtual void Attack()
    {
        cooldown = currentCooldownDuration;
    }
}
