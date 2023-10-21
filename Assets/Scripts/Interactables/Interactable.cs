using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    
    public void Interact(CharacterController player){
        Interaction(player);
    }

    public virtual void Interaction(CharacterController player){
        Debug.Log("Interacting with an interactable");
    }

}
