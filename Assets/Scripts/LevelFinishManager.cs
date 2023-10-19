using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinishManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradeableGUI;
    [SerializeField] private UpgradeableUI upgradeableUI;
    
    public void ShowMenu(){
        upgradeableGUI.SetActive(true);
    }

    public void CloseMenu(){
        upgradeableGUI.SetActive(false);
    }

    public void SetupMenu(UpgradeablesData.Upgradeable upgradeable){
        // TODO: Grab sprite
        Debug.Log("Setting up");
        upgradeableUI.Setup(null, upgradeable);
    }
}
