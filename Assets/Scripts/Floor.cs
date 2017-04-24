using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Floor : MonoBehaviour {

    public MeshRenderer meshRenderer;
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
        meshRenderer.material.color = Color32.Lerp(meshRenderer.material.color, color, Time.deltaTime * flashSpeed);
    }

    public void Flash() {
        meshRenderer.material.color = litColor;
    }
}
