using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour {
    private GameObject player;

    private Transform dialogueCanvas;
    private Transform panel;

    private bool playerIsMovingRightSide;
    private bool isPanelClosed;

    private float xOffset;
    private bool isShown = false;

    [SerializeField] private float horizontalInput;
    [SerializeField] Joystick joystick;
    private void Awake() {
        player = GameObject.Find("Player");

        dialogueCanvas = transform.Find("DialogueCanvas");
        panel = dialogueCanvas.Find("Panel");

        panel.gameObject.SetActive(false);
        
    }




    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player") && !isShown) {
            panel.gameObject.SetActive(true);

            float waitingSeconds = 3;

            StartCoroutine(PanelShownTime(waitingSeconds));
            isShown = true;
        }
    }

    private void Update() {
        UpdatePositionAsAChild();
    }

    private void SideDetection() {
        //horizontalInput = joystick.Horizontal;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput < 0) {
            playerIsMovingRightSide = false;
        } else if (horizontalInput > 0) {
            playerIsMovingRightSide = true;
        }
    }

    private void FollowPlayer() {
        player.GetComponent<PlayerController>().BirdsAttached++;
        //if (playerController.BirdsAttached == 0) {
        //    playerController.AttachBird();
        //}


    }

    private void UpdatePositionAsAChild() {
        SideDetection();

        // TODO: Make birds have seperate controls.

        if (isPanelClosed) {
            //Vector3 offset = new Vector3(3, 0, 0);

            Debug.Log("Worked!");
            float yOffset = 7;
            float lerpSpeed = 1;
            Vector3 targetPosition;

            if (playerIsMovingRightSide) {
                targetPosition = new Vector3(player.transform.position.x - xOffset, player.transform.position.y + yOffset, 0);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
                return;
            }
            targetPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, 0);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);

            Debug.Log($"targetPosition: {targetPosition}");
        }
    }

    


    IEnumerator PanelShownTime(float waitingSeconds) {
        OffsetAssignation();


        yield return new WaitForSeconds(waitingSeconds);
        panel.gameObject.SetActive(false);
        isPanelClosed = true;
        FollowPlayer();
    }

    private void OffsetAssignation() {
        xOffset = player.GetComponent<PlayerController>().BirdsAttached switch {
            0 => 5,
            1 => 8,
            2 => 2,
            _ => 0
        };
    }
}
