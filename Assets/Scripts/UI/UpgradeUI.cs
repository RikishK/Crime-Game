using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText, buttonText;
    [SerializeField] private HorizontalLayoutGroup upgradeIndicatorsLayout;
    [SerializeField] private Sprite upgradedIndicator, availableUpgradeIndicator;
    [SerializeField] private GameObject[] upgradeIndicatorSlots;
    
    public void Setup(UpgradeablesData.UpgradeData upgradeData){
        nameText.text = upgradeData.upgrade_name;
        buttonText.text = $"${upgradeData.upgrade_cost.ToString()}";
        for(int i=0; i<6; i++){
            if (i >= upgradeData.total_upgrades){
                upgradeIndicatorSlots[i].SetActive(false);
                continue;
            }
            
            upgradeIndicatorSlots[i].SetActive(true);

            if (i< upgradeData.upgrades_done){
                upgradeIndicatorSlots[i].GetComponentInChildren<Image>().sprite = upgradedIndicator;
            }
            else{
                upgradeIndicatorSlots[i].GetComponentInChildren<Image>().sprite = availableUpgradeIndicator;
            }
        }

    }

    public void BuyUpgrade(){
        LevelFinishManager levelFinishManager = FindObjectOfType<LevelFinishManager>();
        levelFinishManager.BuyUpgrade(nameText.text);
    }
}
