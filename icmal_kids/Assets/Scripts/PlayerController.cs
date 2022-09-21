using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    [SerializeField] private MaterialTintColor materialTintColor;

    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private UI_Inventory uiInventory;
    private float xInput;
    private float yInput;
    private Rigidbody2D rigidbody2d;
    private Collider2D collider2d;
    public AudioClip itemPickUpSound;
    private int birdsAttached;
    public int BirdsAttached {
        get {

            return birdsAttached;
        }
        set {
            birdsAttached = value;
        }
    }


    private Inventory inventory;

    public void ChangeColor() {
        // There was an issue with coroutines.
        // Default color'll be yellow. Object'll become red for 0.1 seconds



    }

    IEnumerator Waiting(float seconds) {

        yield return new WaitForSeconds(seconds);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;

    }

    private void Awake() {

        rigidbody2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();

    }
    private void Start() {
        inventory = new Inventory(UseItem);
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);
    }

    private void UseItem(Item item) {
        switch (item.itemType) {
            case Item.ItemType.HealthPotion:
                FlashGreen();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
            case Item.ItemType.ManaPotion:
                FlashBlue();
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                break;
        }
    }
    private void ColorChangeControl() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            // ChangeColor();
            Debug.Log(Time.deltaTime);

        }
    }

    private void Update() {
        ColorChangeControl();



        //MovementControl();
        //JumpControl();
        VerticalPositionControl();

    }

    private void VerticalPositionControl() {
        if (transform.position.y <= -45) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void MovementControl() {
        xInput = Input.GetAxis("Horizontal");
        transform.Translate(movementVector * xInput * Time.deltaTime);
    }

    private void JumpControl() {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            float jumpVelocity = 35f;
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }
    }
    public Vector3 GetPosition() {
        return transform.position;
    }
    private bool IsGrounded() {



        Vector2 direction = new Vector2(0, -2);
        ContactFilter2D filter = new ContactFilter2D() {

        };
        RaycastHit2D[] results = new RaycastHit2D[10];
        //float distance;

        int numberOfHits = collider2d.Cast(direction, results);

        for (int i = 0; i < numberOfHits; i++) {
            if (!results[i].collider.isTrigger) {
                return true;
            }
        }
        return false;
        //RaycastHit2D raycastHit2d = Physics2D.BoxCast(collider2d.bounds.center, collider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        // Debug.Log(raycastHit2d.collider);


        // return raycastHit2d.collider != null;

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("PuzzleButtonTrigger")) {
            SoundManager.instance.audioSource.PlayOneShot(itemPickUpSound);
        }

        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null) {

            inventory.AddItem(itemWorld.GetItem());

            itemWorld.DestroySelf();
        }
    }


    public void FlashGreen() {
        materialTintColor.SetTintColor(new Color(0, 1, 0, 1));
    }

    public void FlashRed() {
        materialTintColor.SetTintColor(new Color(1, 0, 0, 1));
    }

    public void FlashBlue() {
        materialTintColor.SetTintColor(new Color(0, 0, 1, 1));
    }

    public void AttachBird() {
        birdsAttached++;
    }

    public void DetachBird() {
        birdsAttached--;
    }
}
