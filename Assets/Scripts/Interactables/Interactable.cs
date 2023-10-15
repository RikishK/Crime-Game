using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    
    public ItemData.ItemType Interact(CharacterController player){
        return Interaction(player);
    }

    public virtual ItemData.ItemType Interaction(CharacterController player){
        Debug.Log("Interacting with an interactable");
        return 0;
    }
    

}
