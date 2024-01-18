using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;
    public int currentExperience;
    public ExpPickup pickup;
    public List<int> expLevels;
    public int currentLevel = 1;
    public int levelCount = 100;
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.2f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;
        if (currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }
        
        UIController.instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
    }

    void LevelUp()
    {
        currentExperience -= expLevels[currentLevel];
        currentLevel++;
        if (currentExperience >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }
        
        UIController.instance.levelUpPanel.SetActive(true);
        Time.timeScale = 0;
        UIController.instance.levelUpButtons[1].UpdateButtonDisplay(PlayerController.instance.activeWeapon);
    }
}
