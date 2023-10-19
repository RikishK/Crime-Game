using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHeldItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer heldItemRenderer;
    [SerializeField] private Sprite Charcoal, Sulfur, Nitrate, Gunpowder, Metal, Bullet_Shell;
    
    public void HoldItem(ItemData.ItemType itemType){
        switch(itemType){
            case ItemData.ItemType.None:
                heldItemRenderer.sprite = null;
                break;
            case ItemData.ItemType.Charcoal:
                heldItemRenderer.sprite = Charcoal;
                break;
            case ItemData.ItemType.Sulfur:
                heldItemRenderer.sprite = Sulfur;
                break;
            case ItemData.ItemType.Nitrate:
                heldItemRenderer.sprite = Nitrate;
                break;
            case ItemData.ItemType.Gunpowder:
                heldItemRenderer.sprite = Gunpowder;
                break;
            case ItemData.ItemType.Metal:
                heldItemRenderer.sprite = Metal;
                break;
            case ItemData.ItemType.Bullet_Shell:
                heldItemRenderer.sprite = Bullet_Shell;
                break;
        }
    }
}
