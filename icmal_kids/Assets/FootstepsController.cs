using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsController : MonoBehaviour
{
    public GameObject footsteps;
    private bool isActive;
    [SerializeField] List<bool> keyboardStates;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        footsteps.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
        WalkControl();
    }

    private void WalkControl() {
        
        

        // bool wPressed = Input.GetKeyDown("w");
        // bool aPressed = Input.GetKeyDown("a");
        // bool sPressed = Input.GetKeyDown("s");
        // bool dPressed = Input.GetKeyDown("d");

        // keyboardStates = new List<bool>() {
            // wPressed, aPressed, sPressed, dPressed
        // };

        // bool walkButtonPressed = false;
        // foreach (bool state in keyboardStates) {
            // while (state) {
                // walkButtonPressed = true;
            // }
        // }
        // if (walkButtonPressed) {
            // Activate();
            // walkButtonPressed = false;
            // return;
        // } 
            // Deactivate();
        


        //bool isWalking = Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d");
        //bool isStoppedWalking = Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d");

        //if (isWalking && !isActive) {
        //    Activate();
        //}
        //if (isStoppedWalking && isActive) {
        //    Deactivate();
        //}

    }
}
