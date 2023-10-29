using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Maker_Upgradeable : Upgradeable
{
    protected override UpgradeablesData.Upgradeable GetUpgradeData()
    {
        return UpgradeablesData.bullet_maker_upgradeable;
    }
}
