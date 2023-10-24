using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour {
    private Transform buttonCanvas;
    private void Awake() {
        buttonCanvas = transform.Find("Canvas_Puzzle");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            buttonCanvas.gameObject.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            buttonCanvas.gameObject.SetActive(false);

        }
    }
}
