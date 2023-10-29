using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : Station
{
    protected override ItemData.ItemType CraftItemType()
    {
        return ItemData.ItemType.Gunpowder;
    }

    protected override bool canCraft(){
        bool c = ingredients[ItemData.ItemType.Charcoal] > 0 && ingredients[ItemData.ItemType.Nitrate] > 0 && ingredients[ItemData.ItemType.Sulfur] > 0;
        bool v = craftedItems[ItemData.ItemType.Gunpowder] < maxCraftedItems[ItemData.ItemType.Gunpowder] && !currentlyCrafting;
        return c && v;
    }

    protected override void craft(){
        Debug.Log("Crafting...");
        ingredients[ItemData.ItemType.Charcoal] -= 1;
        ingredients[ItemData.ItemType.Nitrate] -= 1;
        ingredients[ItemData.ItemType.Sulfur] -= 1;
        StartCoroutine(craftItem(ItemData.ItemType.Gunpowder));
    }

    protected override int StationIngredientMax()
    {
        int upgrades_done = UpgradeablesData.mixer_upgradeable.GetUpgradeData("Ingredient Limit").upgrades_done;
        return 3 + 2*upgrades_done;
    }

    protected override int StationCraftedMax()
    {
        int upgrades_done = UpgradeablesData.mixer_upgradeable.GetUpgradeData("Output Limit").upgrades_done;
        return 3 + 2*upgrades_done;
    }

    protected override float StationCraftTime()
    {
        float upgrades_done = UpgradeablesData.mixer_upgradeable.GetUpgradeData("Crafting Speed").upgrades_done;
        return 7f - 1f*upgrades_done;
    }
}
