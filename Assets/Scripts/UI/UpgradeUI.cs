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
    [SerializeField] private GameObject upgradedIndicator, availableUpgradeIndicator;
    
    public void Setup(UpgradeablesData.UpgradeData upgradeData){
        nameText.text = upgradeData.upgrade_name;
        buttonText.text = $"${upgradeData.upgrade_cost.ToString()}";
        for(int i=0; i<upgradeData.total_upgrades; i++){
            if (i< upgradeData.upgrades_done){
                GameObject upgraded_indicator = Instantiate(upgradedIndicator);
                upgraded_indicator.transform.SetParent(upgradeIndicatorsLayout.transform, false);
            }
            else{
                GameObject available_upgrade_indicator = Instantiate(availableUpgradeIndicator);
                available_upgrade_indicator.transform.SetParent(upgradeIndicatorsLayout.transform, false);
            }
        }

    }
}
