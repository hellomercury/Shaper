using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger: MonoBehaviour {

    public bool hasLost = false;
    private GUIStyle guiStyle = new GUIStyle();

    void OnTriggerEnter2D() {
        Debug.Log("OnTriggerEnter2D");
        hasLost = true;
    }

    void OnGUI() {
        if (hasLost) {
            GUI.color = Color.black;
            guiStyle.fontSize = 50;
            GUI.Label(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 25, 100, 50), "GAME OVER!", guiStyle);

            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 30, 100, 50), "Restart!", guiStyle)) {
                Application.LoadLevel(Application.loadedLevel);
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 45, Screen.height / 2 + 85, 100, 50), "Quit!", guiStyle))
            {
                Application.Quit();
            }
        }
    }
}