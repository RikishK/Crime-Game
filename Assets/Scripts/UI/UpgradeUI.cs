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

    private UpgradeablesData.UpgradeData myUpgradeData;
    
    public void Setup(UpgradeablesData.UpgradeData upgradeData){
        nameText.text = upgradeData.upgrade_name;
        int cost = upgradeData.Cost();
        buttonText.text = $"${cost.ToString()}";
        myUpgradeData = upgradeData;
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
        Debug.Log("Reset Upgrade UI");
        Setup(myUpgradeData);
    }
}
