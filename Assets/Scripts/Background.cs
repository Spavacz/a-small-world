using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public MeshRenderer renderer;
    public float flashSpeed;
    public Color color;
    public Color litColor;

    private Vector3 targetScale;

    void Awake() {
        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.onSpectrum.AddListener(OnSpectrum);
    }

    void Start() {
        targetScale = transform.localScale;
    }

    private void OnSpectrum(float[] spectrum) {
        if(spectrum[5] > 0.04f) {
            Flash();
            RandomSize();
        }
    }

    void Update() {
        renderer.material.color = Color32.Lerp(renderer.material.color, color, Time.deltaTime * flashSpeed);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * flashSpeed);
    }

    public void Flash() {
        renderer.material.color = litColor;
    }

    public void RandomSize() {
        targetScale = new Vector3(Random.Range(6f, 40f), Random.Range(6f, 40f), 1f);
    }
}
