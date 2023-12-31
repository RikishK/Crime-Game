using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : Station
{
    public override ItemData.ItemType Interaction(CharacterController player){
        Debug.Log($"Interacting with Mixer");
        ItemData.ItemType heldItem = player.GetHeldItem();
        if(heldItem == ItemData.ItemType.None){
            if(craftedItems[ItemData.ItemType.Gunpowder] > 0){
                craftedItems[ItemData.ItemType.Gunpowder] -= 1;
                updateLights();
                return ItemData.ItemType.Gunpowder;
            }
        }
        else if(validItem(heldItem)){
            if(ingredients[heldItem] < maxIngredients[heldItem]){
                Debug.Log($"Taking player item: {heldItem}");
                ingredients[player.ConsumeItem()] += 1;
                DebugInventory();
                updateLights();
            }
        }
        return 0;
    }

    protected override bool canCraft(){
        bool c = (ingredients[ItemData.ItemType.Charcoal] > 0 && ingredients[ItemData.ItemType.Nitrate] > 0 && ingredients[ItemData.ItemType.Sulfur] > 0);
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
        float upgrades_done = UpgradeablesData.mixer_upgradeable.GetUpgradeData("Output Limit").upgrades_done;
        return 7f - 1f*upgrades_done;
    }
}
