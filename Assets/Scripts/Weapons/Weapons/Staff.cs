using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        GameObject fireBall = Instantiate(weaponData.prefab);
        fireBall.transform.position = this.transform.position;
        fireBall.GetComponent<KnifeProjectile>().CheckDirection(pm.ShootDir);
    }

    public override void LevelUp()
    {
        base.LevelUp();
    }
}
