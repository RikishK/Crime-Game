using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Setup mixer upgradeable data
        Debug.Log("Setting up mixer data");
        UpgradeablesData.Upgradeable mixer_upgradeable = new UpgradeablesData.Upgradeable();
        mixer_upgradeable.upgradeable_name = "Mixer";
        List<UpgradeablesData.UpgradeData> mixer_upgrades = new List<UpgradeablesData.UpgradeData>();
        UpgradeablesData.UpgradeData mixer_intake_upgrade = new UpgradeablesData.UpgradeData("Ingredient Limit", 0, 4, 100);
        UpgradeablesData.UpgradeData mixer_output_upgrade = new UpgradeablesData.UpgradeData("Output Limit", 0, 4, 100);
        UpgradeablesData.UpgradeData mixer_crafting_speed = new UpgradeablesData.UpgradeData("Crafting Speed", 0, 4, 100);
        mixer_upgrades.Add(mixer_intake_upgrade);
        mixer_upgrades.Add(mixer_output_upgrade);
        mixer_upgrades.Add(mixer_crafting_speed);
        mixer_upgradeable.upgradesData = mixer_upgrades;
        UpgradeablesData.mixer_upgradeable = mixer_upgradeable;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
