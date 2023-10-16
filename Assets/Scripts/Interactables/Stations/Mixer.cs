using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : Station
{
    //private Dictionary<ItemData.ItemType, int> craftingItems;
    //[SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer CharcoalLight, NitrateLight, SulfurLight, GunpowderLight;
    [SerializeField] private Sprite emptyLight, lowLight, mediumLight, fullLight;
    //private int outputGunPowder = 0;
    //public int maxIngredient = 3;
    
    //private bool currentlyCrafting = false;

    // private void Start() {
    //     craftingItems = new Dictionary<ItemData.ItemType, int>();
    //     craftingItems.Add(ItemData.ItemType.Charcoal, 0);
    //     craftingItems.Add(ItemData.ItemType.Sulfur, 0);
    //     craftingItems.Add(ItemData.ItemType.Nitrate, 0);
    // }

    protected override void SetupStation()
    {
        // Setup ingredients dictionary
        ingredients = new Dictionary<ItemData.ItemType, int>();
        ingredients.Add(ItemData.ItemType.Charcoal, 0);
        ingredients.Add(ItemData.ItemType.Sulfur, 0);
        ingredients.Add(ItemData.ItemType.Nitrate, 0);

        // Setup max Ingredients dictionary
        maxIngredients = new Dictionary<ItemData.ItemType, int>();
        maxIngredients.Add(ItemData.ItemType.Charcoal, 3);
        maxIngredients.Add(ItemData.ItemType.Sulfur, 3);
        maxIngredients.Add(ItemData.ItemType.Nitrate, 3);

        // Setup crafted Items dictionary
        craftedItems = new Dictionary<ItemData.ItemType, int>();
        craftedItems.Add(ItemData.ItemType.Gunpowder, 0);

        // Setup max crafted items dictionary
        maxCraftedItems = new Dictionary<ItemData.ItemType, int>();
        maxCraftedItems.Add(ItemData.ItemType.Gunpowder, 3);

        // Setup crafted items timer dictionary
        craftedItemsTime = new Dictionary<ItemData.ItemType, float>();
        craftedItemsTime.Add(ItemData.ItemType.Gunpowder, 7.0f);
    }

    public override ItemData.ItemType Interaction(CharacterController player){
        Debug.Log($"Interacting with Mixer");
        ItemData.ItemType heldItem = player.GetHeldItem();
        if(heldItem == ItemData.ItemType.None){
            if(base.craftedItems[ItemData.ItemType.Gunpowder] > 0){
                base.craftedItems[ItemData.ItemType.Gunpowder] -= 1;
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

    protected override void updateLights(){
        CharcoalLight.sprite = getLightLevel(ingredients[ItemData.ItemType.Charcoal], maxIngredients[ItemData.ItemType.Charcoal]);
        NitrateLight.sprite = getLightLevel(ingredients[ItemData.ItemType.Nitrate], maxIngredients[ItemData.ItemType.Nitrate]);
        SulfurLight.sprite = getLightLevel(ingredients[ItemData.ItemType.Sulfur], maxIngredients[ItemData.ItemType.Sulfur]);
        GunpowderLight.sprite = getLightLevel(craftedItems[ItemData.ItemType.Gunpowder], maxCraftedItems[ItemData.ItemType.Gunpowder]);
    }

    private Sprite getLightLevel(int curr, int max){
        float percentage = curr / max;
        if(curr == 0) return emptyLight;
        if(curr == max) return fullLight;
        if(percentage < 0.3f) return lowLight;
        return mediumLight;
    }

    // private void Update() {
    //     // Sink
    //     if(!canCraft()) return;

    //     craft();
        
    // }

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

    // private IEnumerator craftGunPowder(){
    //     currentlyCrafting = true;
    //     anim.SetBool("mixing", true);
    //     yield return new WaitForSeconds(7f);
    //     currentlyCrafting = false;
    //     anim.SetBool("mixing", false);
    //     outputGunPowder += 1;
    //     updateLights();
    // }

    // protected override IEnumerator craftItem(ItemData.ItemType craftedItemType){
    //     currentlyCrafting = true;
    //     anim.SetBool("mixing", true);
    //     yield return new WaitForSeconds(craftedItemsTime[craftedItemType]);
    //     currentlyCrafting = false;
    //     anim.SetBool("mixing", false);
    //     outputGunPowder += 1;
    //     updateLights();
    // }
}
