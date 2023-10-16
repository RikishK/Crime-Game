using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    [SerializeField] private CharacterHeldItem heldItem;
    private ItemData.ItemType currentItem;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentItem = new ItemData.ItemType();
    }

    void Update()
    {
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
                holdItem(targetInteractables[0].Interact(this));
                Debug.Log($"Player has picked up: {currentItem}");
            }

        }
    }

    private void holdItem(ItemData.ItemType itemType){
        currentItem = itemType;
        heldItem.HoldItem(currentItem);
    }
    public ItemData.ItemType GetHeldItem(){
        return currentItem;
    }

    public ItemData.ItemType ConsumeItem(){
        ItemData.ItemType consumedItem = currentItem;
        currentItem = ItemData.ItemType.None;
        return consumedItem;
    }
}
