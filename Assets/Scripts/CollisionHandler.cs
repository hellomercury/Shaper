using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

    void OnCollisionEnter2D() {
        Vector3 camPos = Camera.main.transform.position;
        if (camPos.y < transform.position.y) {
            Camera.main.GetComponent<CameraMover>().targetY = transform.position.y;
        }
    }
}
