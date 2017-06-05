using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {

    Rigidbody2D grabbedObject = null;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            //Clicked
            Vector3 MouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 MousePos2D = new Vector2(MouseWorldPos3D.x, MouseWorldPos3D.y);
            Vector2 Direction = Vector2.zero;

            RaycastHit2D HitTest = Physics2D.Raycast(MousePos2D, Direction);
            if (HitTest && HitTest.collider != null) {
                //clicked on something with collider
                if (HitTest.collider.GetComponent<Rigidbody2D>() != null) {
                    grabbedObject = HitTest.collider.GetComponent<Rigidbody2D>();
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            grabbedObject = null;
        }
    }

    void FixedUpdate() {
        if (grabbedObject != null) {
            //moves object with mouse
            Vector3 MouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 MousePos2D = new Vector2(MouseWorldPos3D.x, MouseWorldPos3D.y);

            grabbedObject.position = MousePos2D;
        }
    }
}
