using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelFinishManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradeableGUI;
    [SerializeField] private UpgradeableUI upgradeableUI;
    [SerializeField] private TextMeshProUGUI moneytext;

    private UpgradeablesData.Upgradeable currentUpgradeable;
    
    private void Start() {
        UpdateMoneyText();
    }

    private void UpdateMoneyText(){
        moneytext.text = $"${GameDetails.player_money.ToString()}";
    }
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
        int cost = upgradeData.Cost();
        if(cost > GameDetails.player_money){
            // Play an animation for cant afford
            Debug.Log("cant afford");
            return;
        }
        Debug.Log($"Buying Upgrade {upgrade_name} of {currentUpgradeable.upgradeable_name}");
        if (upgradeData.upgrades_done == upgradeData.total_upgrades) return;
        GameDetails.player_money -= cost;
        upgradeData.upgrades_done += 1;
        UpdateMoneyText();
        // TODO: Take away money
    }

    public void Continue(){
        GameDetails.current_level++;
        SceneManager.LoadScene("LevelCountdown", LoadSceneMode.Single);
    }
}
