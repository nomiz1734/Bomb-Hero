using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EXPLVController : MonoBehaviour
{
    public static EXPLVController instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int LvThreshold = 5;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject spawnEnemy;
    [SerializeField] private AudioManager audioManager;
    private bool bossSpawned = false;


    private void Awake()
    {
        instance = this;
    }
    public int currentExperience;
    public EXPPickup pickup;

    public List<int> expLevels;
    public int currentLevel = 1, levelCount = 100;
    void Start()
    {
        boss.SetActive(false);
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetExp(int amount)
    {
        currentExperience += amount;
        Debug.Log("Current Experience: " + currentExperience);
        if(currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }
        UIController.instance.UpdateExp(currentExperience, expLevels[currentLevel], currentLevel);
    }

    public void SpawnPickup(Vector3 position, int value)
    {
        Instantiate(pickup, position, Quaternion.identity).expValue=value;
    }
    private void CallBoss() { 
        audioManager.PlayBossMusic();
        bossSpawned= true;
        boss.SetActive(true);
        spawnEnemy.SetActive(false);
    }
    void LevelUp()
    {
        if (bossSpawned) { 
            return;
        }
        currentExperience -= expLevels[currentLevel];
        currentLevel++;
        Debug.Log("Leveled up to level: " + currentLevel);
        if (currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1; 
        }
        if (currentLevel == LvThreshold)
        {
            CallBoss();
        }
        GameManager.instance.activeWeapon.LevelUp();
    }
}
