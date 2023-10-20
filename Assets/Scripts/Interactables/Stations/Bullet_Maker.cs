using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Maker : Station
{
    public override ItemData.ItemType Interaction(CharacterController player){

        Debug.Log("Interacting with Bullet_Maker");
        ItemData.ItemType heldItem = player.GetHeldItem();

        if(heldItem == ItemData.ItemType.None){
            if(craftedItems[ItemData.ItemType.Bullet_Shell] > 0){
                craftedItems[ItemData.ItemType.Bullet_Shell] -= 1;
                updateLights();
                return ItemData.ItemType.Bullet_Shell;
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

        return ItemData.ItemType.None;

    }
    protected override bool canCraft(){    
        bool c = ingredients[ItemData.ItemType.Metal] > 0;
        bool v = craftedItems[ItemData.ItemType.Bullet_Shell] < maxCraftedItems[ItemData.ItemType.Bullet_Shell] && !currentlyCrafting;
        return c && v;
    }

    protected override void craft(){
        Debug.Log("Crafting...");
        Debug.Log("Crafting...");
        ingredients[ItemData.ItemType.Metal] -= 1;
        StartCoroutine(craftItem(ItemData.ItemType.Bullet_Shell));
    }

    protected override int StationIngredientMax()
    {
        int upgrades_done = UpgradeablesData.bullet_maker_upgradeable.GetUpgradeData("Ingredient Limit").upgrades_done;
        return 3 + 2*upgrades_done;
    }

    protected override int StationCraftedMax()
    {
        int upgrades_done = UpgradeablesData.bullet_maker_upgradeable.GetUpgradeData("Output Limit").upgrades_done;
        return 3 + 2*upgrades_done;
    }

    protected override float StationCraftTime()
    {
        float upgrades_done = UpgradeablesData.bullet_maker_upgradeable.GetUpgradeData("Output Limit").upgrades_done;
        return 7f - 1f*upgrades_done;
    }
    

}
