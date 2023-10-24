using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;

    private float horizontalMovement;
    private Rigidbody2D playerRigidbody;
    float movePower = 10f;
    bool isGrounded = false;
    private float direction;

    private Animator anim;
    bool isJumping;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // PlayerHorizontalMovement();
        //JumpControl();
        Run();
        Jump();
    }


    void Run()
    {
        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isRunning", false);


        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -.8f;
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(direction, .8f, 1);
            // I think this row below implements not running while jumping.
            if (!anim.GetBool("isJumping"))
                anim.SetBool("isRunning", true);
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = .8f;
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(direction, .8f, 1);
            if (!anim.GetBool("isJumping"))
                anim.SetBool("isRunning", true);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;

        Debug.Log("moveVelocity: " + moveVelocity);
        Debug.Log("movePower: " + movePower);
    }

    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space) || Input.GetAxisRaw("Vertical") > 0)
            && !anim.GetBool("isJumping"))
        {
            //if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("isJumping")) {

            isJumping = true;
            anim.SetBool("isJumping", true);
        }

        if (!isJumping)
        {
            return;
        }

        //playerRigidbody.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpForce);
        //playerRigidbody.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }


    private void PlayerHorizontalMovement()
    {
        horizontalMovement = movementSpeed * Input.GetAxis("Horizontal");
        transform.Translate(horizontalMovement, 0, 0);

        WalkingAnimation();
    }

    private void WalkingAnimation()
    {
        if (horizontalMovement != 0)
        {
            GetComponent<Animator>().SetTrigger("walk");
            return;
        }

        GetComponent<Animator>().SetTrigger("idle");
    }

    private void JumpControl()
    {
        bool isJumpable = Input.GetKey(KeyCode.Space) && isGrounded;

        if (isJumpable)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }

        anim.SetBool("isJumping", false);
    }
}