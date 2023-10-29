using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Station : Interactable
{
    protected Dictionary<ItemData.ItemType, int> ingredients, maxIngredients, craftedItems, maxCraftedItems;
    protected Dictionary<ItemData.ItemType, float> craftedItemsTime;
    [SerializeField] protected Animator anim;
    [SerializeField] protected string stationName;
    
    [SerializeField] protected StationItemSlot[] stationItemSlots;

    protected Dictionary<ItemData.ItemType, IndicatorLight> itemIndicatorsDictionary;

    [Serializable]
    protected class StationItemSlot {
        public IndicatorLight indicatorLight;
        public ItemData.ItemType item;
        public SlotType slotType;

    }

    [Serializable]
    protected enum SlotType {
        Input, Output
    }

    protected bool currentlyCrafting = false;

    protected virtual void SetupStation(){
        Debug.Log($"Setting up a station {name}");
        // Setup Ingredients
        // Setup max Ingredients
        // Setup crafted items
        // Setup max crafted items
        // Setup crafted items time
        ingredients = new Dictionary<ItemData.ItemType, int>();
        maxIngredients = new Dictionary<ItemData.ItemType, int>();
        craftedItems = new Dictionary<ItemData.ItemType, int>();
        maxCraftedItems = new Dictionary<ItemData.ItemType, int>();
        craftedItemsTime = new Dictionary<ItemData.ItemType, float>();
        itemIndicatorsDictionary = new Dictionary<ItemData.ItemType, IndicatorLight>();
        
        foreach(StationItemSlot itemSlot in stationItemSlots){
            itemIndicatorsDictionary.Add(itemSlot.item, itemSlot.indicatorLight);
            if(itemSlot.slotType == SlotType.Input){
                ingredients.Add(itemSlot.item, 0);
                maxIngredients.Add(itemSlot.item, StationIngredientMax());
            }
            else if(itemSlot.slotType == SlotType.Output){
                craftedItems.Add(itemSlot.item, 0);
                maxCraftedItems.Add(itemSlot.item, StationCraftedMax());
                craftedItemsTime.Add(itemSlot.item, StationCraftTime());
            }
        }
        DebugInventory();

    }

    public override void Interaction(CharacterController player){

        Debug.Log("Interacting with Bullet_Maker");
        ItemData.ItemType heldItem = player.GetHeldItem();
        ItemData.ItemType craftItem = CraftItemType();
        if(heldItem == ItemData.ItemType.None){
            if(craftedItems[craftItem] > 0){
                GiveItem(craftItem, player);
                updateLights();
            }
        }
        else if(heldItem == craftItem){
            if(craftedItems[craftItem] > 0){
                GiveItemTopup(craftItem, player);
                updateLights();
            }
        }
        else if(validItem(heldItem)){
            if(ingredients[heldItem] < maxIngredients[heldItem]){
                Debug.Log($"Taking player item: {heldItem}");
                int ingredient_space_left = maxIngredients[heldItem] - ingredients[heldItem];
                int player_ingredient_count = player.CarryCount();
                int consume_amount = Mathf.Min(ingredient_space_left, player_ingredient_count);
                ingredients[player.ConsumeItem(consume_amount)] += consume_amount;
                DebugInventory();
                updateLights();
            }
        }
    }

    protected virtual ItemData.ItemType CraftItemType(){
        return ItemData.ItemType.None;
    }

    protected void TakeIngredient(ItemData.ItemType ingredientType, CharacterController player){
        Debug.Log("TAKING");
        int player_ingredient_amount = player.CarryCount();
        int ingredient_space_left = maxIngredients[ingredientType] - ingredients[ingredientType];
    }

    protected void GiveItem(ItemData.ItemType itemType, CharacterController player){
        int give_amount = GiveAmount(itemType, player);
        player.ReceiveItem(itemType, give_amount);
        craftedItems[itemType] -= give_amount;
    }

    protected void GiveItemTopup(ItemData.ItemType itemType, CharacterController player){
        int give_topup_amount = GiveTopupAmount(itemType, player);
        player.ReceiveItemTopup(itemType, give_topup_amount);
        craftedItems[itemType] -= give_topup_amount;
    }

    protected int GiveTopupAmount(ItemData.ItemType itemType, CharacterController player){
        int curr_amount = craftedItems[itemType];
        int player_space_left = player.CarryCapacity() - player.CarryCount();
        return Mathf.Min(curr_amount, player_space_left);
    }

    protected int GiveAmount(ItemData.ItemType itemType, CharacterController player){
        int curr_amount = craftedItems[itemType];
        int player_carry_capacity = player.CarryCapacity();
        return Mathf.Min(curr_amount, player_carry_capacity);
    }

    protected virtual int StationIngredientMax()
    {
        return 3;
    }

    protected virtual int StationCraftedMax()
    {
        return 3;
    }

    protected virtual float StationCraftTime()
    {
        return 7.0f;
    }

    private void Start() {
        SetupStation();
    }

    private void Update() {
        // Sink
        if(!canCraft()) return;

        craft();
    }

    protected virtual bool validItem(ItemData.ItemType itemType){
        foreach(KeyValuePair<ItemData.ItemType, int> pair in ingredients){
            if (itemType == pair.Key) return true;
        }
        return false;
    }

    protected virtual void DebugInventory(){
        Debug.Log($"Inventory of {stationName}");
        foreach(KeyValuePair<ItemData.ItemType, int> pair in ingredients){
            Debug.Log(pair);
        }
        Debug.Log("-----------------------------------------");
        foreach(KeyValuePair<ItemData.ItemType, int> pair in craftedItems){
            Debug.Log(pair);
        }
        Debug.Log("=========================================");

    }

    protected virtual void updateLights(){
        Debug.Log("Updating station lights...");
        foreach(KeyValuePair<ItemData.ItemType, int> ingredientData in ingredients){
            itemIndicatorsDictionary[ingredientData.Key].updateLight(ingredientData.Value, maxIngredients[ingredientData.Key]);
        }
        foreach(KeyValuePair<ItemData.ItemType, int> craftedData in craftedItems){
            itemIndicatorsDictionary[craftedData.Key].updateLight(craftedData.Value, maxCraftedItems[craftedData.Key]);
        }
    }

    protected virtual void UpdateImage(){
        // 
    }

    protected virtual bool canCraft(){
        Debug.Log("Checing station can craft...");
        return false;
    }

    protected virtual void craft(){
        Debug.Log("Station is crafting...");
    }

    protected virtual IEnumerator craftItem(ItemData.ItemType craftedItemType){
        Debug.Log($"Station ${name} is crafting {craftedItemType}");
        currentlyCrafting = true;
        anim.SetBool("crafting", true);
        updateLights();
        yield return new WaitForSeconds(craftedItemsTime[craftedItemType]);
        currentlyCrafting = false;
        anim.SetBool("crafting", false);
        craftedItems[craftedItemType] += 1;
        updateLights();
    }
}
