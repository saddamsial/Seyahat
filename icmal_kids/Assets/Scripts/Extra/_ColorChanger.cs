using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ColorChanger : MonoBehaviour {

    public GameObject player;

    // Start is called before the first frame update
    void Start() {
        _EventHandling.OnButtonPushed += ChangeColor;
    }

    private void ChangeColor() {
        player.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
