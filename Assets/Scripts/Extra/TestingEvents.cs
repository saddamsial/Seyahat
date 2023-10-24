using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEvents : MonoBehaviour {
    // Our event'll containt and object of OnSpacePressedEventArgs on second parameter.
    public event EventHandler<OnSpacePressedEventArgs> OnSpacePressed;

    public event TestEventDelegate OnFloatEvent;
    public delegate void TestEventDelegate(float f);

    public event Action<bool, int> OnActionEvent;
    public class OnSpacePressedEventArgs : EventArgs {
        public int spaceCount;
    }
    private int spaceCount;
    private void Start() {

    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Space pressed!
            if (OnSpacePressed != null) {
                spaceCount++;
                // OnSpacePressed(this, EventArgs.Empty);
                // Invokes the event only if OnSpacePressed is not null.
                OnSpacePressed?.Invoke(this, new OnSpacePressedEventArgs { spaceCount = spaceCount });
                OnFloatEvent?.Invoke(5.5f);
                OnActionEvent?.Invoke(true, 56);

            }
        }
    }
}
