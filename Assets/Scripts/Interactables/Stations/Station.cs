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

        public int maxItem;
        public float craftTimer = 0f;

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
                maxIngredients.Add(itemSlot.item, itemSlot.maxItem);
            }
            else if(itemSlot.slotType == SlotType.Output){
                craftedItems.Add(itemSlot.item, 0);
                maxCraftedItems.Add(itemSlot.item, itemSlot.maxItem);
                craftedItemsTime.Add(itemSlot.item, itemSlot.craftTimer);
            }
        }
        DebugInventory();

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
