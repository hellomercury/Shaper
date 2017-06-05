using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {

    public bool useSpring = false;

    public LineRenderer dragLine;
   float velocityRatio = 4f;
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

                    if(useSpring) {
                        springJoint = grabbedObject.gameObject.AddComponent<SpringJoint2D>();
                        //set anchor at object part
                        Vector3 localhitPoint = grabbedObject.transform.InverseTransformPoint(hitTest.point);
                        springJoint.anchor = localhitPoint;
                        springJoint.connectedAnchor = mouseWorldPos3D;
                        springJoint.distance = 0.1f;
                        springJoint.dampingRatio = 1;
                        springJoint.frequency = 5;
                        springJoint.autoConfigureDistance = false;
                        springJoint.enableCollision = true;
                        springJoint.connectedBody = null;
                    } else {
                        //velocity instead spring
                        grabbedObject.gravityScale = 0;
                    }

                
                    dragLine.enabled = true;
                }
            }
        }
        //if grabbed something
        if (Input.GetMouseButtonUp(0) && grabbedObject != null) {
            if(useSpring) {
                Destroy(springJoint);
                springJoint = null;
            } else {
                grabbedObject.gravityScale = 1;
            }
            
            grabbedObject = null;
            dragLine.enabled = false;
        }
    }

    void FixedUpdate() {
        if (grabbedObject != null) {
            //moves object with mouse
            Vector2 mouseWorldPos2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (useSpring) {
                springJoint.connectedAnchor = mouseWorldPos2D;
            } else {
                grabbedObject.velocity = (mouseWorldPos2D - grabbedObject.position) * velocityRatio;
            }
            /*
            Vector2 mousePos2D = new Vector2(mouseWorldPos3D.x, mouseWorldPos3D.y);

            Vector2 dir = mousePos2D - grabbedObject.position;

            dir *= velocityRatio;
            
            grabbedObject.velocity = dir;
            */

          
        }
    }
    void LateUpdate() {
        if (grabbedObject != null) {
            if (useSpring) {
                Vector3 worldAnchor = grabbedObject.transform.TransformPoint(springJoint.anchor);
                dragLine.SetPosition(0, new Vector3(worldAnchor.x, worldAnchor.y, -1));
                dragLine.SetPosition(1, new Vector3(springJoint.connectedAnchor.x, springJoint.connectedAnchor.y, -1));
            } else {
                Vector3 mouseWorldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dragLine.SetPosition(0, new Vector3(grabbedObject.position.x, grabbedObject.position.y, -1));
                dragLine.SetPosition(1, new Vector3(mouseWorldPos3D.x, mouseWorldPos3D.y, -1));
            }
        }
    }
}
