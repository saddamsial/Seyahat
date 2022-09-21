using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField] AudioClip puzzleSolvedSound;

    public static SoundManager instance;

    public AudioSource audioSource;

    private void Awake() {
        if (SoundManager.instance != null) {
            return;
        }
        instance = this;

        audioSource = GetComponent<AudioSource>();
    }
    

}
