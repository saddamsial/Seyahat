using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _EventHandling : MonoBehaviour
{
    public delegate void ButtonPush();
    public static event ButtonPush OnButtonPushed;

    public void PushControl() {
        OnButtonPushed();
    }
}
