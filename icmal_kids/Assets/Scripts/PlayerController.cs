using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private UI_Inventory uiInventory;
    private float xInput;
    private float yInput;
    private Rigidbody2D rigidbody2d;
    private CapsuleCollider2D capsuleCollider2d;

    private Inventory inventory;

    private void Awake() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        capsuleCollider2d = GetComponent<CapsuleCollider2D>();
        inventory = new Inventory();
    }

    private void Start() {
        uiInventory.SetInventory(inventory);
    }

    private void Update() {
        MovementControl();
        JumpControl();

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

    private bool IsGrounded() {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(capsuleCollider2d.bounds.center, capsuleCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        // Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null) {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

}
