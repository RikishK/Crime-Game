using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradeable : MonoBehaviour
{
    [SerializeField] protected string upgradeable_name;
    private void OnMouseDown() {
        // Find levle finish manager and set the gui for upgrades
        LevelFinishManager levelFinishManager = FindObjectOfType<LevelFinishManager>();
        levelFinishManager.ShowMenu();
        levelFinishManager.SetupMenu(GetUpgradeData());
    }

    protected virtual UpgradeablesData.Upgradeable GetUpgradeData(){
        return null;
    }
    
}
