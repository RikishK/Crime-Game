using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Interactable
{
    private CrateState crateState = CrateState.Empty;
    [SerializeField] private SpriteRenderer crateRenderer;
    [SerializeField] private Sprite emptyCrate, empty_bullet_crate, loaded_bullet_crate, packaged_crate;
    [SerializeField] ObjectiveStatus objectiveStatus;
    private enum CrateState {
        Empty, Empty_Bullet_Shell, Loaded_Bullet_Shell, Packaged
    }
    public override void Interaction(CharacterController player){

        Debug.Log("Interacting with Crate");

        if(validItem(player.GetHeldItem())){
            handleInteract(player);
        }

    }

    private bool validItem(ItemData.ItemType itemType){
        switch(crateState){
            case CrateState.Empty:
                return itemType == ItemData.ItemType.Bullet_Shell;
            case CrateState.Empty_Bullet_Shell:
                return itemType == ItemData.ItemType.Gunpowder;
            case CrateState.Loaded_Bullet_Shell:
                return itemType == ItemData.ItemType.None;
        }
        return false;
    }

    private void handleInteract(CharacterController player){
        player.ConsumeItem(1);
        switch(crateState){
            case CrateState.Empty:
                crateState = CrateState.Empty_Bullet_Shell;
                RenderCrate();
                break;
            case CrateState.Empty_Bullet_Shell:
                crateState = CrateState.Loaded_Bullet_Shell;
                RenderCrate();
                break;
            case CrateState.Loaded_Bullet_Shell:
                StartCoroutine(PackageUp(player));
                break;
        }
        
    }

    private IEnumerator PackageUp(CharacterController player){
        player.LockPlayer();
        yield return new WaitForSeconds(player.PackageSpeed());
        player.UnlockPlayer();
        crateState = CrateState.Packaged;
        RenderCrate();
        objectiveStatus.Complete();
    }

    private void RenderCrate(){
        switch (crateState){
            case CrateState.Empty:
                crateRenderer.sprite = emptyCrate;
                break;
            case CrateState.Empty_Bullet_Shell:
                crateRenderer.sprite = empty_bullet_crate;
                break;
            case CrateState.Loaded_Bullet_Shell:
                crateRenderer.sprite = loaded_bullet_crate;
                break;
            case CrateState.Packaged:
                crateRenderer.sprite = packaged_crate;
                break;
        }
    }
}
