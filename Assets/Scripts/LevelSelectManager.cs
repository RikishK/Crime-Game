using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public void LevelStart(){
        // Setup mixer upgradeable data
        UpgradeablesData.mixer_upgradeable = SetupStation("mixer");

        // Setup bullet_maker upgradeable data
        UpgradeablesData.bullet_maker_upgradeable = SetupStation("bullet_maker");

        // Setup player upgradeable data
        UpgradeablesData.player_upgradeable = SetupPlayer();

        SceneManager.LoadScene("PlayScene", LoadSceneMode.Single);
    }

    private UpgradeablesData.Upgradeable SetupStation(string station_name){
        Debug.Log($"Setting up {station_name} data");
        UpgradeablesData.Upgradeable station_upgradeable = new UpgradeablesData.Upgradeable();
        station_upgradeable.upgradeable_name = station_name;
        List<UpgradeablesData.UpgradeData> station_upgrades = new List<UpgradeablesData.UpgradeData>();
        UpgradeablesData.UpgradeData station_intake_upgrade = new UpgradeablesData.UpgradeData("Ingredient Limit", 0, 4, 100);
        UpgradeablesData.UpgradeData station_output_upgrade = new UpgradeablesData.UpgradeData("Output Limit", 0, 4, 100);
        UpgradeablesData.UpgradeData station_crafting_speed = new UpgradeablesData.UpgradeData("Crafting Speed", 0, 4, 100);
        station_upgrades.Add(station_intake_upgrade);
        station_upgrades.Add(station_output_upgrade);
        station_upgrades.Add(station_crafting_speed);
        station_upgradeable.upgradesData = station_upgrades;
        return station_upgradeable;
    }

    private UpgradeablesData.Upgradeable SetupPlayer(string player_name = "player"){
        Debug.Log($"Setting up {player_name} data");
        UpgradeablesData.Upgradeable player_upgradeable = new UpgradeablesData.Upgradeable();
        player_upgradeable.upgradeable_name = player_name;
        List<UpgradeablesData.UpgradeData> player_upgrades = new List<UpgradeablesData.UpgradeData>();
        UpgradeablesData.UpgradeData player_speed_upgrade = new UpgradeablesData.UpgradeData("Move Speed", 0, 4, 100);
        UpgradeablesData.UpgradeData player_carry_capacity_upgrade = new UpgradeablesData.UpgradeData("Carry Capacity", 0, 4, 100);
        UpgradeablesData.UpgradeData player_package_speed_upgrade = new UpgradeablesData.UpgradeData("Package Speed", 0, 4, 100);
        player_upgrades.Add(player_speed_upgrade);
        player_upgrades.Add(player_carry_capacity_upgrade);
        player_upgrades.Add(player_package_speed_upgrade);
        player_upgradeable.upgradesData = player_upgrades;
        return player_upgradeable;
    }
}
