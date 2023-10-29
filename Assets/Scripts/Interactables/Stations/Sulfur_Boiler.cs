using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sulfur_Boiler : Station
{
    protected override ItemData.ItemType CraftItemType()
    {
        return ItemData.ItemType.Sulfuric_Acid;
    }

    protected override bool canCraft(){
        bool c = ingredients[ItemData.ItemType.Sulfur] > 0;
        bool v = craftedItems[ItemData.ItemType.Sulfuric_Acid] < maxCraftedItems[ItemData.ItemType.Sulfuric_Acid] && !currentlyCrafting;
        return c && v;
    }

    protected override void craft(){
        Debug.Log("Crafting...");
        ingredients[ItemData.ItemType.Sulfur] -= 1;
        StartCoroutine(craftItem(ItemData.ItemType.Sulfuric_Acid));
    }

    protected override int StationIngredientMax()
    {
        int upgrades_done = UpgradeablesData.sulfur_boiler_upgradeable.GetUpgradeData("Ingredient Limit").upgrades_done;
        return 1 + 1*upgrades_done;
    }

    protected override int StationCraftedMax()
    {
        int upgrades_done = UpgradeablesData.sulfur_boiler_upgradeable.GetUpgradeData("Output Limit").upgrades_done;
        return 1 + 1*upgrades_done;
    }

    protected override float StationCraftTime()
    {
        float upgrades_done = UpgradeablesData.sulfur_boiler_upgradeable.GetUpgradeData("Crafting Speed").upgrades_done;
        return 7f - 1f*upgrades_done;
    }
}
