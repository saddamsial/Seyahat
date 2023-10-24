using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicPC : MonoBehaviour
{

    public float movePower = 10f;
    public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5
    private Rigidbody2D rb;
    private Animator anim;
    Vector3 movement;
    private float direction = .8f;
    public bool isWalking;
    [SerializeField] bool isJumping = false;
    private bool alive = true;
    [SerializeField] bool wallGrab;
    PlayerCollision playerCollision;
    [SerializeField] float speedModifier;
    private AudioSource audioSource;
    public AudioSource climbingAudioSource;
    public bool canMove = true;
    public bool wallJumping;
    public AudioClip walkSound;
    public AudioClip landingSound;
    public AudioClip jumpingSound;
    public AudioClip climbingSound;
    public AudioClip slidingFromWallSound;
    public AudioClip itemPickUpSound;
    private int jumpCount = 0;
    private bool isStartedPlaying = false;
    [SerializeField] TextMeshProUGUI textJumpCount;
    [SerializeField] float verticalMovement;
    [SerializeField] float horizontalMovement;


    //[SerializeField] Transform parentTransform;

    void Start()
    {

        //parentTransform.position = transform.position;
        audioSource = GetComponent<AudioSource>();
        playerCollision = GetComponent<PlayerCollision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //verticalStop = new Vector2(rb.velocity.x, 0);
    }


    private void Update()
    {
        //Restart();
        if(alive)
        {
            WallGrab();
            //Hurt();
            //Die();
            Attack();
            Jump();
            Walk();
            ShowJumpCount();

        }
    }

    private void ShowJumpCount()
    {
        textJumpCount.text = $"Jump Count: " + jumpCount;

    }

    private void WallGrab()
    {
        //verticalMovement = joystick.Vertical;
        verticalMovement = Input.GetAxisRaw("Vertical");
        if(playerCollision.onWall && anim.GetBool("isJumping"))
        {
            wallGrab = true;
        }
        anim.SetBool("GrabWall", wallGrab);

        if(!playerCollision.onWall)
        {
            wallGrab = false;
        }

        if(playerCollision.onGround)
        {
            wallGrab = false;

        }

        if(wallGrab)
        {
            rb.gravityScale = 0;
            //rb.velocity = verticalStop;

            speedModifier = verticalMovement > 0 ? .35f : 1;
            rb.velocity = new Vector2(rb.velocity.x, verticalMovement * (speedModifier * movePower));
            anim.SetFloat("VerticalAxis", verticalMovement);

            //if (rb.velocity.y > 0) {
            //    if (!climbingAudioSource.isPlaying) {
            //        climbingAudioSource.PlayOneShot(climbingSound);
            //    } else {
            //        climbingAudioSource.Stop();
            //    }

            //}
            //if (rb.velocity.y < 0) {
            //    if (!climbingAudioSource.isPlaying) {
            //        climbingAudioSource.PlayOneShot(slidingFromWallSound);
            //    } else {
            //        climbingAudioSource.Stop();
            //    }
            //}

            isJumping = false;

            if(Input.GetKeyDown("w"))
            {
                climbingAudioSource.PlayOneShot(climbingSound);
            }
            if(Input.GetKeyUp("w"))
            {
                climbingAudioSource.Stop();
            }
            if(Input.GetKeyDown("s"))
            {
                climbingAudioSource.PlayOneShot(slidingFromWallSound);
            }
            if(Input.GetKeyUp("s"))
            {
                climbingAudioSource.Stop();
            }

        }
        else
        {
            rb.gravityScale = 5;
            climbingAudioSource.Stop();

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(anim.GetBool("isJumping"))
        {

            if(collision.gameObject.CompareTag("Platform"))
            {

                anim.SetBool("isJumping", false);
                anim.SetBool("isLanded", true);
                SoundManager.instance.audioSource.PlayOneShot(landingSound);

                if(jumpCount == 1)
                {
                    jumpCount--;
                }

            }
        }
    }


    public Joystick joystick;
    void Walk()
    {
        if(!canMove)
        {
            return;
        }
        if(wallGrab)
        {
            return;
        }

        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isRunning", false);


        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            //if (joystick.Horizontal < 0) {


            direction = -0.8f;
            moveVelocity = Vector3.left;

            //transform.Find("NPC").localScale = new Vector3(-direction, .8f, 1);
            transform.localScale = new Vector3(direction, .8f, 1);
            // I think this row below implements not running while jumping.
            if(!anim.GetBool("isJumping"))
                anim.SetBool("isRunning", true);

        }
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            //if (joystick.Horizontal > 0) {


            direction = .8f;
            moveVelocity = Vector3.right;

            //transform.Find("NPC").localScale = new Vector3(direction, .8f, 1);
            transform.localScale = new Vector3(direction, .8f, 1);

            if(!anim.GetBool("isJumping"))
                anim.SetBool("isRunning", true);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;


        if(playerCollision.onGround && anim.GetBool("isRunning"))
        {
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(walkSound, .7f);
            }
        }
        else
        {
            audioSource.Stop();
        }

        //if (anim.GetBool("isRunning") && !isStartedPlaying) {
        //    isStartedPlaying = true;
        //    GetComponent<AudioSource>().PlayOneShot(groundLandSound);
        //    Debug.Log("Played!");
        //} else {
        //    isStartedPlaying = false;
        //}
        //else {
        //    GetComponent<AudioSource>().Stop();
        //    Debug.Log("Stoped!");
        //}

        if(playerCollision.onGround)
        {
            wallJumping = false;
        }


        if(!isJumping)
        {
            return;
        }

    }
    public void msg()
    {
        Debug.Log("Worked!");
    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(jumpCount == 0)
            {

                // TODO add -- version and configure this method.
                jumpCount++;
                //if ((Input.GetButtonDown("Jump"))) {
                //}

                isJumping = true;
                anim.SetBool("isJumping", true);
                if(!playerCollision.onGround && playerCollision.onWall)
                {
                    anim.SetBool("isJumping", true);
                    jumpCount--;
                    return;
                }

                if(wallGrab && !wallJumping)
                {
                    WallJump();
                }
                else
                {

                    rb.velocity = Vector2.zero;

                    Vector2 jumpVelocity = new Vector2(0, jumpPower);
                    rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
                    isJumping = false;
                    SoundManager.instance.audioSource.PlayOneShot(jumpingSound);

                }
            }
        }
    }
    void WallJump()
    {
        StartCoroutine(DisableMovement(.1f));
        Vector2 wallDirection = playerCollision.onRightWall ? Vector2.left : Vector2.right;
        //Vector2 wallJumpVector = new Vector2(, jumpPower);
        rb.AddForce(3 * (Vector2.up + wallDirection), ForceMode2D.Impulse);
        wallJumping = true;
        SoundManager.instance.audioSource.PlayOneShot(jumpingSound);

    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        wallJumping = true;
        yield return new WaitForSeconds(time);
        canMove = true;
        wallJumping = false;
    }
    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("attack");
        }
    }


    //void Hurt() {
    //    if (Input.GetKeyDown(KeyCode.Alpha2)) {
    //        anim.SetTrigger("hurt");
    //        if (direction == 1)
    //            rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
    //        else
    //            rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
    //    }
    //}
    //void Die() {
    //    if (Input.GetKeyDown(KeyCode.Alpha3)) {
    //        anim.SetTrigger("die");
    //        alive = false;
    //    }
    //}
    //void Restart() {
    //    if (Input.GetKeyDown(KeyCode.Alpha0)) {
    //        anim.SetTrigger("idle");
    //        alive = true;
    //    }
    //}
}
