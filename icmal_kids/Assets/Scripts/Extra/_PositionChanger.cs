using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PositionChanger : MonoBehaviour
{
    public GameObject player;

    private void Start() {
        _EventHandling.OnButtonPushed += ChangePosition;
    }

    private void ChangePosition() {
        player.transform.Translate(0, 10, 0);
    }
}
