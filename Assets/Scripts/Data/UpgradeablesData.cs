using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeablesData
{
    public static Upgradeable mixer_upgradeable;
    public static Upgradeable bullet_maker_upgradeable;

    public static Upgradeable player_upgradeable;

    public class Upgradeable {
        public string upgradeable_name;
        public List<UpgradeData> upgradesData; 

        public UpgradeData GetUpgradeData(string upgrade_name){
            foreach(UpgradeData upgradeData in upgradesData){
                if(upgradeData.upgrade_name == upgrade_name) return upgradeData;
            }
            return null;
        }
    }
    public class UpgradeData {
        public string upgrade_name;
        public int upgrades_done;
        public int total_upgrades;
        public int upgrade_cost;

        public UpgradeData(string upgrade_name, int upgrades_done, int total_upgrades, int upgrade_cost){
            this.upgrade_name = upgrade_name;
            this.upgrades_done = upgrades_done;
            this.total_upgrades = total_upgrades;
            this.upgrade_cost = upgrade_cost;
        }

        public int Cost(){
            return upgrade_cost * (upgrades_done + 1);
        }
    }

}
