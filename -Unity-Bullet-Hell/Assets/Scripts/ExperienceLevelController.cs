using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;
    public int currentExperience;
    public ExpPickup pickup;
    public List<int> expLevels;
    public int currentLevel = 1;
    public int levelCount = 100;
    public List<Weapon> weaponsToUpgrade;
    
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
        weaponsToUpgrade.Clear();
        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(PlayerController.instance.assignedWeapons);
        if (availableWeapons.Count > 0)
        {
            int selected = Random.Range(0, availableWeapons.Count);
            weaponsToUpgrade.Add(availableWeapons[selected]);
            availableWeapons.RemoveAt(selected);
        }

        if (PlayerController.instance.assignedWeapons.Count + PlayerController.instance.fullyLeveledWeapons.Count < PlayerController.instance.maxWeapons)
        {
            availableWeapons.AddRange(PlayerController.instance.unassignedWeapons);
        }

        for (int i = weaponsToUpgrade.Count; i < 3; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int selected = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }

        for (int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            UIController.instance.levelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        }

        for (int i = 0; i < UIController.instance.levelUpButtons.Length; i++)
        {
            if (i < weaponsToUpgrade.Count)
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIController.instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }
        
        PlayerStatController.instance.UpdateDisplay();
    }
}
