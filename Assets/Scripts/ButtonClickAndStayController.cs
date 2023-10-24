using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonClickAndStayController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private bool isPointerDown;
    private float pointerDownTimer;
    public float requiredHoldTime;
    // public UnityEvent onLongClick;
    // [SerializeField] private Image fillImage;


    public delegate void ButtonClickAndStay();
    public event ButtonClickAndStay OnLongClick;
    public void OnPointerDown(PointerEventData eventData) {
        isPointerDown = true;

        Debug.Log("OnPointerDown");

    }

    public void OnPointerUp(PointerEventData eventData) {
        isPointerDown=false;

        Debug.Log("OnPointerUp");

    }

    private void Update() {
        if (isPointerDown) {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= requiredHoldTime) {
                
                OnLongClick?.Invoke();
                Reset();                
            }
            // fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
        }
    }

    private void Reset() {
        
        isPointerDown = false;
        pointerDownTimer = 0;
        // fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }
    
    
    
}
