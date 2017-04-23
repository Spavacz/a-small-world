using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EqualizerLit : MonoBehaviour {

    private Renderer meshRenderer;

    public float speed;

    public Color color {
        set {
            meshRenderer.material.color = value;
        }
        private get {
            return meshRenderer.material.color;
        }
    }

    void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update() {
        Color currentColor = color;
        if(currentColor != Color.clear) {
            color = Color.Lerp(currentColor, Color.clear, Time.deltaTime * speed);
        }
    }
}
