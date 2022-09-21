using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public void RestartScene() {
        SceneController.instance.RestartScene();
    }
}
