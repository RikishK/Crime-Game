using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chemical_Mixer : Station
{
    [SerializeField] private SpriteRenderer chemical_mixer_image;
    [SerializeField] private Sprite sulfuric_acid_filled, nitric_acid_filled, empty;
    protected override ItemData.ItemType CraftItemType()
    {
        return ItemData.ItemType.Nitrating_Mixture;
    }

    protected override bool canCraft(){
        bool c = ingredients[ItemData.ItemType.Sulfuric_Acid] > 0 && ingredients[ItemData.ItemType.Nitric_Acid] > 0;
        bool v = craftedItems[ItemData.ItemType.Nitrating_Mixture] < maxCraftedItems[ItemData.ItemType.Nitrating_Mixture] && !currentlyCrafting;
        return c && v;
    }

    protected override void craft(){
        Debug.Log("Crafting...");
        ingredients[ItemData.ItemType.Nitric_Acid] -= 1;
        ingredients[ItemData.ItemType.Sulfuric_Acid] -= 1;
        StartCoroutine(craftItem(ItemData.ItemType.Nitrating_Mixture));
    }

    protected override int StationIngredientMax()
    {
        int upgrades_done = UpgradeablesData.chemical_mixer_upgradeable.GetUpgradeData("Ingredient Limit").upgrades_done;
        return 1 + 1*upgrades_done;
    }

    protected override int StationCraftedMax()
    {
        int upgrades_done = UpgradeablesData.chemical_mixer_upgradeable.GetUpgradeData("Output Limit").upgrades_done;
        return 1 + 1*upgrades_done;
    }

    protected override float StationCraftTime()
    {
        float upgrades_done = UpgradeablesData.chemical_mixer_upgradeable.GetUpgradeData("Crafting Speed").upgrades_done;
        return 7f - 1f*upgrades_done;
    }

    protected override void UpdateImage(){
        Debug.Log("quack quack");
        if(ingredients[ItemData.ItemType.Sulfuric_Acid] > 0){
            Debug.Log("Filled with sulfuric acid");
            chemical_mixer_image.sprite = sulfuric_acid_filled;
        } 
        else if(ingredients[ItemData.ItemType.Nitric_Acid] > 0){
            Debug.Log("Filled with nitric acid");
            chemical_mixer_image.sprite = nitric_acid_filled;
        }
        else{
            Debug.Log("Empty Chemical Mixer");
            chemical_mixer_image.sprite = empty; 
        }
    }
}
