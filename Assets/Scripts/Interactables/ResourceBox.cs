using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;


public class ResourceBox : Interactable
{
    [SerializeField] private ItemData.ItemType resource;
    public override void Interaction(CharacterController player){
        Debug.Log($"Interacting with resourcebox: {resource}");
        if(player.GetHeldItem() == ItemData.ItemType.None){
            GiveItem(player);
        }
        else if(player.GetHeldItem() == resource){
            GiveItemTopup(player);
        }

    }

    protected virtual void GiveItem(CharacterController player){
        int give_amount = player.CarryCapacity();
        player.ReceiveItem(resource, give_amount);
    }

    protected virtual void GiveItemTopup(CharacterController player){
        int give_topup_amount = player.CarryCapacity() - player.CarryCount();
        player.ReceiveItemTopup(resource, give_topup_amount);
    }
}
