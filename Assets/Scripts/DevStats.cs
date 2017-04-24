using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevStats : MonoBehaviour {

    public Color color;
    public GameSequence sequence;

    private GUIStyle style;
    private int w;
    private int h;

    void Start() {
        w = Screen.width;
        h = Screen.height * 2 / 50;

        style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h;
        style.normal.textColor = color;
    }

    void OnGUI() {
        Rect rect = new Rect(0, h, w, h);
        string text = string.Format("Seq event: {0}", sequence.currentIndex);
        GUI.Label(rect, text, style);

        rect = new Rect(0, 2 * h, w, h);
        text = string.Format("Time: {0:0.0}sec", sequence.currentTime);
        GUI.Label(rect, text, style);
    }
}
