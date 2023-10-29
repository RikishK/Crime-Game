using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCrate : Interactable
{
    private CrateState crateState = CrateState.Empty;
    [SerializeField] private SpriteRenderer crateRenderer;
    [SerializeField] private Sprite emptyCrate, gunpowder_filled_crate, nitrated_gunpowder_crate, explosive_wrapped_crate, packaged_crate;
    [SerializeField] ObjectiveStatus objectiveStatus;
    private enum CrateState {
        Empty, Gunpowder_Filled, Nitrated_Gunpowder, Wrapped, Packaged
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
                return itemType == ItemData.ItemType.Gunpowder;
            case CrateState.Gunpowder_Filled:
                return itemType == ItemData.ItemType.Nitrating_Mixture;
            case CrateState.Nitrated_Gunpowder:
                return itemType == ItemData.ItemType.None;
            case CrateState.Wrapped:
                return itemType == ItemData.ItemType.None;

        }
        return false;
    }

    private void handleInteract(CharacterController player){
        player.ConsumeItem(1);
        switch(crateState){
            case CrateState.Empty:
                crateState = CrateState.Gunpowder_Filled;
                RenderCrate();
                break;
            case CrateState.Gunpowder_Filled:
                crateState = CrateState.Nitrated_Gunpowder;
                RenderCrate();
                break;
            case CrateState.Nitrated_Gunpowder:
                StartCoroutine(WrapUp(player));
                break;
            case CrateState.Wrapped:
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
        StartCoroutine(Finish());
    }

    private IEnumerator WrapUp(CharacterController player){
        player.LockPlayer();
        yield return new WaitForSeconds(player.PackageSpeed());
        player.UnlockPlayer();
        crateState = CrateState.Wrapped;
        RenderCrate();
    }

    private IEnumerator Finish(){
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void RenderCrate(){
        switch (crateState){
            case CrateState.Empty:
                crateRenderer.sprite = emptyCrate;
                break;
            case CrateState.Gunpowder_Filled:
                crateRenderer.sprite = gunpowder_filled_crate;
                break;
            case CrateState.Nitrated_Gunpowder:
                crateRenderer.sprite = nitrated_gunpowder_crate;
                break;
            case CrateState.Wrapped:
                crateRenderer.sprite = explosive_wrapped_crate;
                break;
            case CrateState.Packaged:
                crateRenderer.sprite = packaged_crate;
                break;
        }
    }
}
