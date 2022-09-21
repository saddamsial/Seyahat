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

    [SerializeField] private float horizontalInput;
    private void Awake() {
        player = GameObject.Find("Player");

        dialogueCanvas = transform.Find("DialogueCanvas");
        panel = dialogueCanvas.Find("Panel");

        panel.gameObject.SetActive(false);
    }




    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            panel.gameObject.SetActive(true);

            float waitingSeconds = 3;

            StartCoroutine(PanelShownTime(waitingSeconds));
        }
    }

    private void Update() {
        UpdatePositionAsAChild();
    }

    private void SideDetection() {
        horizontalInput = Input.GetAxis("Horizontal");
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
        transform.parent = player.transform;

        
    }

    private void UpdatePositionAsAChild() {
        SideDetection();

        // TODO: Make birds have seperate controls.

        if (isPanelClosed) {
            //Vector3 offset = new Vector3(3, 0, 0);
            
            float yOffset = 7;
            float lerpSpeed = 1;
            Vector3 targetPosition;



            if (playerIsMovingRightSide) {
                targetPosition = new Vector3(player.transform.position.x - xOffset, player.transform.position.y + yOffset, 0);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
                return;
            }
            targetPosition = new Vector3((player.transform.position.x + xOffset), player.transform.position.y + yOffset, 0);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);

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
            2 => 1,
            _ => 0
        };
    }
}
