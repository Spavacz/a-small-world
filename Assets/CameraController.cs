using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    public Color litColor;
    public Color darkColor;

    public float flashSpeed;

    private Color color;
    private Camera camera;
    private AudioProcessor processor;

    void Awake() {
        camera = GetComponent<Camera>();
        processor = GetComponent<AudioProcessor>();
    }

    void Start() {
        processor.onBeat.AddListener(OnBeatDetected);
        processor.onSpectrum.AddListener(OnSpectrum);
    }

    private void OnBeatDetected() {
        //Flash();
    }

    private void OnSpectrum(float[] spectrum) {
        if(spectrum[5] > 0.04f) {
            Flash();
        }
        for(int i = 0; i < spectrum.Length; ++i) {
            Vector3 start = new Vector3(i, 0, 0);
            Vector3 end = new Vector3(i, spectrum[i] * 100f, 0);
            Debug.DrawLine(start, end, Color.yellow);
        }
    }

    void Update() {
        camera.backgroundColor = Color32.Lerp(camera.backgroundColor, darkColor, Time.deltaTime * flashSpeed);
    }

    public void Flash() {
        camera.backgroundColor = litColor;
    }
}
