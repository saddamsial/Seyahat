using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tileset_PlayerController : MonoBehaviour {

    [SerializeField] private LayerMask platformsLayerMask;

    private float horizontalInput;
    private float verticalInput;
    private float movementSpeed = 5f;

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;



    private int number;
    public int Number {
        get { return number; }
        set { number = value; }
    }

    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * movementSpeed * Time.deltaTime);
    }

    private void JumpControl() {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            float jumpVelocity = 35f;
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }
    }

    private bool IsGrounded() {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        // Debug.Log(raycastHit2d.collider);
        return raycastHit2d.collider != null;
    }
}
