using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameSequence))]
public class SequenceEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if(GUILayout.Button("Generate sequence")) {
            GameSequence gameSequence = (GameSequence)target;
            gameSequence.GenerateSequence();
        }
    }

}
