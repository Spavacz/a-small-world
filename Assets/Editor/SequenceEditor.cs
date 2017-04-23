using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameSequence))]
public class SequenceEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GameSequence gameSequence = (GameSequence)target;
        if(GUILayout.Button("Generate sequence")) {
            gameSequence.GenerateSequence();
        }
    }

}
