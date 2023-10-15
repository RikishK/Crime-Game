using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;


public class ResourceBox : Interactable
{
    [SerializeField] private ItemData.ItemType resource;
    public override ItemData.ItemType Interaction(CharacterController player){
        Debug.Log($"Interacting with resourcebox: {resource}");
        return resource;
    }
}
