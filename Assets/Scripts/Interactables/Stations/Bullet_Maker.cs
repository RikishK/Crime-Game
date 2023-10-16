using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Maker : Interactable
{
    private int bullet_shell_count = 0;
    private int metal_count = 0;
    private int max_metal = 3;
    private int max_bullet_shell = 3;
    private bool currentlyCrafting = false;

    [SerializeField] private SpriteRenderer MetalLight, Bullet_ShellLight;
    [SerializeField] private Sprite emptyLight, lowLight, mediumLight, fullLight;
    [SerializeField] private Animator anim;
    public override ItemData.ItemType Interaction(CharacterController player){

        Debug.Log("Interacting with Bullet_Maker");

        if(player.GetHeldItem() == ItemData.ItemType.None){
            if(bullet_shell_count > 0){
                bullet_shell_count--;
                updateLights();
                return ItemData.ItemType.Bullet_Shell;
            }
        }

        if(player.GetHeldItem() != ItemData.ItemType.Metal) return 0;
        if(metal_count < max_metal){
            player.ConsumeItem();
            metal_count++;
            updateLights();
        }

        return ItemData.ItemType.None;

    }

    private void updateLights(){
        MetalLight.sprite = getLightLevel(metal_count, max_metal);
        Bullet_ShellLight.sprite = getLightLevel(bullet_shell_count, max_bullet_shell);
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
        return metal_count > 0 && bullet_shell_count < max_bullet_shell && !currentlyCrafting;
    }

    private void craft(){
        Debug.Log("Crafting...");
        metal_count--;
        StartCoroutine(craftBulletShell());
    }

    private IEnumerator craftBulletShell(){
        currentlyCrafting = true;
        anim.SetBool("smashing", true);
        yield return new WaitForSeconds(5f);
        currentlyCrafting = false;
        anim.SetBool("smashing", false);
        bullet_shell_count += 1;
        updateLights();
    }
}
