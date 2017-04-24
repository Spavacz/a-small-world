using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevStats : MonoBehaviour {

    public static bool isDev = false;

    public GameObject devPanel;
    public Color color;
    public GameSequence sequence;
    public Animator vixaAnimator;

    private GUIStyle style;
    private int w;
    private int h;

    void Awake() {
        if(isDev) {
            devPanel.SetActive(true);
        }
    }

    void Start() {
        w = Screen.width;
        h = Screen.height * 2 / 50;

        style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h;
        style.normal.textColor = color;
    }

    void OnGUI() {
        if(isDev) {
            Rect rect = new Rect(0, h, w, h);
            string text = string.Format("Seq event: {0}", sequence.currentIndex);
            GUI.Label(rect, text, style);

            rect = new Rect(0, 2 * h, w, h);
            text = string.Format("seq time: {0:0.0}sec", sequence.currentTime);
            GUI.Label(rect, text, style);

            AnimatorStateInfo state = vixaAnimator.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] clip = vixaAnimator.GetCurrentAnimatorClipInfo(0);
            float animTime = clip[0].clip.length * state.normalizedTime;

            rect = new Rect(0, 3 * h, w, h);
            text = string.Format("anim time: {0:0.0}sec", animTime);
            GUI.Label(rect, text, style);
        }
    }
}
