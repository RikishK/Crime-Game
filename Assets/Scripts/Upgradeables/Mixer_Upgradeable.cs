using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer_Upgradeable : Upgradeable
{
    protected override UpgradeablesData.Upgradeable GetUpgradeData()
    {
        return UpgradeablesData.mixer_upgradeable;
    }
}
