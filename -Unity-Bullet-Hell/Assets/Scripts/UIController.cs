using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider expLvlSlider;
    public TMP_Text expLvlText;
    public LevelUpSelectionButton[] levelUpButtons;
    public GameObject levelUpPanel;
    public TMP_Text coinText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateExperience(int currentExp, int levelExp, int currentLevel)
    {
        expLvlSlider.maxValue = levelExp;
        expLvlSlider.value = currentExp;
        expLvlText.text = "Level: " + currentLevel;
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void UpdateCoins()
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }
}
