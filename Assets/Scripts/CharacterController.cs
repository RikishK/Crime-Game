using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    [SerializeField] private CharacterHeldItem heldItem;
    private int carry_capacity = 1, carry_count = 0;
    private float package_speed = 5.0f;
    private ItemData.ItemType currentItem;
    private Rigidbody2D rb;

    private bool locked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentItem = new ItemData.ItemType();
        moveSpeed = 5.0f + 2.0f * UpgradeablesData.player_upgradeable.GetUpgradeData("Move Speed").upgrades_done;
        carry_capacity = 1 + UpgradeablesData.player_upgradeable.GetUpgradeData("Carry Capacity").upgrades_done;
        package_speed = 5.0f - 0.5f*UpgradeablesData.player_upgradeable.GetUpgradeData("Package Speed").upgrades_done;
    }

    void Update()
    {
        if(locked){
            rb.velocity = new Vector2(0f, 0f);
            return;
        }
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * moveSpeed;

        // Apply movement to the Rigidbody
        rb.velocity = movement;

        // Rotate the character towards the mouse cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

        // Interact
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 center = transform.position;
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, 1.5f);

            List<Interactable> targetInteractables = new List<Interactable>();

            foreach (Collider2D col in hitColliders)
            {
                if (col.CompareTag("Interactable"))
                {
                    // Sink
                    mousePosition.z = 0;
                    float distance = Vector3.Distance(mousePosition,col.transform.position);
                    if(distance < 0.6f) targetInteractables.Add(col.gameObject.GetComponent<Interactable>());
                }
            }

            if(targetInteractables.Count == 1){
                targetInteractables[0].Interact(this);
                Debug.Log($"Player has picked up: {currentItem}");
            }

        }

        if (Input.GetKeyDown(KeyCode.Q)){
            currentItem = ItemData.ItemType.None;
            heldItem.HoldItem(ItemData.ItemType.None);
        }
    }

    public void ReceiveItem(ItemData.ItemType itemType, int amount){
        currentItem = itemType;
        carry_count = amount;
        heldItem.HoldItem(currentItem);
    }

    public void ReceiveItemTopup(ItemData.ItemType itemType, int topup_amount){
        carry_count += topup_amount;
    }
    public ItemData.ItemType GetHeldItem(){
        return currentItem;
    }

    public ItemData.ItemType ConsumeItem(int amount){
        carry_count -= amount;
        ItemData.ItemType consumed_item = currentItem;
        if (carry_count <= 0){
            currentItem = ItemData.ItemType.None;
            heldItem.HoldItem(ItemData.ItemType.None);
        }
        return consumed_item;
    }

    public void LockPlayer(){
        locked = true;
    }

    public void UnlockPlayer(){
        locked = false;
    }

    public int CarryCapacity(){
        return carry_capacity;
    }

    public int CarryCount(){
        return carry_count;
    }

    public float PackageSpeed(){
        return package_speed;
    }
}
