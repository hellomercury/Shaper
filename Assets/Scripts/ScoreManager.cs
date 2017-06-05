using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public int score = 0;

    void OnGUI() {
        GUI.color = Color.black;
        GUI.Label(new Rect(0, 0, 100, 50), "Score: " + score);
    }

}
