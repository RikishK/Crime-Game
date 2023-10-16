using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : Interactable
{
    private Dictionary<ItemData.ItemType, int> craftingItems;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer CharcoalLight, NitrateLight, SulfurLight, GunpowderLight;
    [SerializeField] private Sprite emptyLight, lowLight, mediumLight, fullLight;
    private int outputGunPowder = 0;
    public int maxIngredient = 3;
    
    private bool currentlyCrafting = false;

    private void Start() {
        craftingItems = new Dictionary<ItemData.ItemType, int>();
        craftingItems.Add(ItemData.ItemType.Charcoal, 0);
        craftingItems.Add(ItemData.ItemType.Sulfur, 0);
        craftingItems.Add(ItemData.ItemType.Nitrate, 0);
    }

    private bool validItem(ItemData.ItemType itemType){
        return itemType == ItemData.ItemType.Charcoal || itemType == ItemData.ItemType.Nitrate || itemType == ItemData.ItemType.Sulfur;
    }
    public override ItemData.ItemType Interaction(CharacterController player){
        Debug.Log($"Interacting with Mixer");
        if(player.GetHeldItem() == ItemData.ItemType.None){
            if(outputGunPowder > 0){
                outputGunPowder -= 1;
                updateLights();
                return ItemData.ItemType.Gunpowder;
            }
        }
        else if(validItem(player.GetHeldItem())){
            if(craftingItems[player.GetHeldItem()] < maxIngredient){
                Debug.Log($"Taking player item: {player.GetHeldItem()}");
                craftingItems[player.ConsumeItem()] += 1;
                DebugInventory();
                updateLights();
            }
        }
        return 0;
    }

    private void DebugInventory(){
        Debug.Log($"Charcoal: {craftingItems[ItemData.ItemType.Charcoal]}, Nitrate: {craftingItems[ItemData.ItemType.Nitrate]}, Sulfur: {craftingItems[ItemData.ItemType.Sulfur]}");
    }

    private void updateLights(){
        CharcoalLight.sprite = getLightLevel(craftingItems[ItemData.ItemType.Charcoal], maxIngredient);
        NitrateLight.sprite = getLightLevel(craftingItems[ItemData.ItemType.Nitrate], maxIngredient);
        SulfurLight.sprite = getLightLevel(craftingItems[ItemData.ItemType.Sulfur], maxIngredient);
        GunpowderLight.sprite = getLightLevel(outputGunPowder, maxIngredient);
    }

    private Sprite getLightLevel(int curr, int max){
        float percentage = curr / max;
        if(curr == 0) return emptyLight;
        if(curr == max) return fullLight;
        if(percentage < 0.3f) return lowLight;
        return mediumLight;
    }

    private void Update() {
        // Sink
        if(!canCraft()) return;

        craft();
        
    }

    private bool canCraft(){
        bool c = (craftingItems[ItemData.ItemType.Charcoal] > 0 && craftingItems[ItemData.ItemType.Nitrate] > 0 && craftingItems[ItemData.ItemType.Sulfur] > 0);
        bool v = outputGunPowder < 3 && !currentlyCrafting;
        return c && v;
    }

    private void craft(){
        Debug.Log("Crafting...");
        craftingItems[ItemData.ItemType.Charcoal] -= 1;
        craftingItems[ItemData.ItemType.Nitrate] -= 1;
        craftingItems[ItemData.ItemType.Sulfur] -= 1;
        StartCoroutine(craftGunPowder());
    }

    private IEnumerator craftGunPowder(){
        currentlyCrafting = true;
        anim.SetBool("mixing", true);
        yield return new WaitForSeconds(2f);
        currentlyCrafting = false;
        anim.SetBool("mixing", false);
        outputGunPowder += 1;
        updateLights();
    }
}
