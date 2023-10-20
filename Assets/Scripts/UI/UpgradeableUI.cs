using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeableUI : MonoBehaviour
{
    [SerializeField] private RawImage upgradeable_icon;
    [SerializeField] private TextMeshProUGUI upgradeable_name; 
    [SerializeField] private VerticalLayoutGroup upgrades_layout;
    [SerializeField] private GameObject[] UpgradeUI_Objects;

    
    public void Setup(Texture icon, UpgradeablesData.Upgradeable upgradeable){
        // TODO: enable once sprites are gathered
        //upgradeable_icon.texture = icon;
        upgradeable_name.text = upgradeable.upgradeable_name;
        // foreach(UpgradeablesData.UpgradeData upgradeData in upgradeable.upgradesData){
        //     Debug.Log("Setting up an upgrade");
        //     GameObject upgradeUI_object = Instantiate(UpgradeUI_Object);
        //     upgradeUI_object.transform.SetParent(upgrades_layout.transform, false);
        //     UpgradeUI upgradeUI = upgradeUI_object.GetComponent<UpgradeUI>();
        //     upgradeUI.Setup(upgradeData);
        // }
        for(int i=0; i< 5; i++){
            if (i>= upgradeable.upgradesData.Count){
                UpgradeUI_Objects[i].SetActive(false);
                continue;
            }
            UpgradeUI_Objects[i].SetActive(true);
            UpgradeablesData.UpgradeData upgradeData = upgradeable.upgradesData[i];
            UpgradeUI_Objects[i].GetComponent<UpgradeUI>().Setup(upgradeData);
        }
    }
}
