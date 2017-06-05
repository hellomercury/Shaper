using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {

    public LineRenderer dragLine;
    float dragSpeed = 4f;
    Rigidbody2D grabbedObject = null;
    SpringJoint2D springJoint = null;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            //Clicked
            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);
            Vector2 dir = Vector2.zero;

            RaycastHit2D hitTest = Physics2D.Raycast(mousePos2D, dir);
            if (hitTest && hitTest.collider != null) {
                //clicked on something with collider
                if (hitTest.collider.GetComponent<Rigidbody2D>() != null) {
                    grabbedObject = hitTest.collider.GetComponent<Rigidbody2D>();

                    springJoint = grabbedObject.gameObject.AddComponent<SpringJoint2D>();
                    //set anchor at object part
                    Vector3 localhitPoint = grabbedObject.transform.InverseTransformPoint(hitTest.point);
                    springJoint.anchor = localhitPoint;
                    springJoint.connectedAnchor = mouseWorldPos3D;
                    springJoint.distance = 0.1f;
                    springJoint.dampingRatio = 0.25f;
                    springJoint.frequency = 0.5f;
                    springJoint.autoConfigureDistance = false;
                    springJoint.enableCollision = true;

                    //springJoint.connectedBody = null;
                
                    dragLine.enabled = true;
                }
            }
        }
        //if grabbed something
        if (Input.GetMouseButtonUp(0) && grabbedObject != null) {
            Destroy(springJoint);
            springJoint = null;
            grabbedObject = null;
            dragLine.enabled = false;
        }
        if(springJoint!= null) {
            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            springJoint.connectedAnchor = mouseWorldPos3D;
        }
    }
    /*
    void FixedUpdate() {
        if (grabbedObject != null) {
            //moves object with mouse
            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);

            Vector2 dir = mousePos2D - grabbedObject.position;

            dir *= dragSpeed;
            
            grabbedObject.velocity = dir;



        }
    }*/
    void LateUpdate() {
        if (grabbedObject != null) {
            Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);
            dragLine.SetPosition(0, new Vector3(grabbedObject.position.x, grabbedObject.position.y, -1));
            dragLine.SetPosition(1, new Vector3(mousePos2D.x, mousePos2D.y, 0));
        }
    }
}
