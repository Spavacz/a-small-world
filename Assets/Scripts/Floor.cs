using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Floor : MonoBehaviour {

    public MeshRenderer renderer;
    public float flashSpeed;
    public Color color;
    public Color litColor;

    void Awake() {
        AudioProcessor processor = FindObjectOfType<AudioProcessor>();
        processor.onSpectrum.AddListener(OnSpectrum);
    }

    private void OnSpectrum(float[] spectrum) {
        if(spectrum[5] > 0.04f) {
            Flash();
        }
    }

    void Update() {
        renderer.material.color = Color32.Lerp(renderer.material.color, color, Time.deltaTime * flashSpeed);
    }

    public void Flash() {
        renderer.material.color = litColor;
    }
}
