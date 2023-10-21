using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinishManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradeableGUI;
    [SerializeField] private UpgradeableUI upgradeableUI;

    private UpgradeablesData.Upgradeable currentUpgradeable;
    
    public void ShowMenu(){
        upgradeableGUI.SetActive(true);
    }

    public void CloseMenu(){
        upgradeableGUI.SetActive(false);
    }

    public void SetupMenu(UpgradeablesData.Upgradeable upgradeable){
        // TODO: Grab sprite
        Debug.Log("Setting up");
        currentUpgradeable = upgradeable;
        upgradeableUI.Setup(null, upgradeable);
    }

    public void BuyUpgrade(string upgrade_name){
        UpgradeablesData.UpgradeData upgradeData = currentUpgradeable.GetUpgradeData(upgrade_name);
        if(upgradeData.upgrade_cost > GameDetails.player_money){
            // Play an animation for cant afford
            return;
        }
        Debug.Log($"Buying Upgrade {upgrade_name} of {currentUpgradeable.upgradeable_name}");
        if (upgradeData.upgrades_done == upgradeData.total_upgrades) return;
        upgradeData.upgrades_done += 1;
        // TODO: Take away money
    }
}
