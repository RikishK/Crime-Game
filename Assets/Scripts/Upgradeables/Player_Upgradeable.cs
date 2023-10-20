using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Upgradeable : Upgradeable
{
    protected override UpgradeablesData.Upgradeable GetUpgradeData()
    {
        return UpgradeablesData.player_upgradeable;
    }
}
