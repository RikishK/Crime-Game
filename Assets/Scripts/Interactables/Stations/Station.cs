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


    protected bool currentlyCrafting = false;

    protected virtual void SetupStation(){
        Debug.Log("Setting up a station...");
        // Setup Ingredients
        // Setup max Ingredients
        // Setup crafted items
        // Setup max crafted items
        // Setup crafted items time
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
        Debug.Log("---------------");
    }

    protected virtual void updateLights(){
        Debug.Log("Updating station lights...");
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
