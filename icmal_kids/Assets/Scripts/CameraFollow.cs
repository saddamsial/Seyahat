using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private float followSpeed;
    [SerializeField] private float yOffSet;
    [SerializeField] private Transform target;

    private void Update() {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y + yOffSet, -10f);
        transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
    }

}

