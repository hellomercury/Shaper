using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger: MonoBehaviour {

    public bool hasLost = false;

    void OnTriggerEnter2D() {
        Debug.Log("OnTriggerEnter2D");
        hasLost = true;
    }

    void OnGUI() {
        if (hasLost) {
            GUI.color = Color.black;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "GAME OVER!");

            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 , 200, 200), "Restart!")) {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}