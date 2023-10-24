using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class TempButtonManager : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip imgActClip;

    public void ActivateImage()
    {
        image.gameObject.SetActive(false);
        image.gameObject.SetActive(true);
        audioSource.PlayOneShot(imgActClip);
    }
}
