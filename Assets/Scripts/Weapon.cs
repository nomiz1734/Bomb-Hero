using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> stats;
    public int weaponLevel;

    [HideInInspector]
    public bool statsUpdated;

    public void LevelUp() { 
        if (weaponLevel < stats.Count - 1) {
            weaponLevel+=1;
            Debug.Log("Weapon leveled up to level: " + weaponLevel);
            statsUpdated = true;
        }
    }    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetWeaponLevel(int level)
    {
                if (level >= 0 && level < stats.Count - 1) {
            weaponLevel =+ level;
            statsUpdated = true;
            Debug.Log("Weapon level set to: " + weaponLevel);
        } else {
            Debug.LogWarning("Invalid weapon level: " + level);
        }
    }
}
[System.Serializable]
public class WeaponStats
{
    public float speed,damage,range, cooldown, amount, duration;
}
