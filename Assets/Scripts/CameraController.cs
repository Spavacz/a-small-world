using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    public float flashSpeed;

    public bool flashEnabled = true;
    public Color litColor;
    public Color darkColor;

    public bool zoomEnabled = true;
    public float camSize;
    public float camZoomSize;

    private Color color;
    private Camera camera;
    private AudioProcessor processor;

    void Awake() {
        camera = GetComponent<Camera>();
        processor = GetComponent<AudioProcessor>();
    }

    void Start() {
        processor.onSpectrum.AddListener(OnSpectrum);
    }

    private void OnSpectrum(float[] spectrum) {
        if(spectrum[5] > 0.04f) {
            Flash();
            Zoom();
        }
//        for(int i = 0; i < spectrum.Length; ++i) {
//            Vector3 start = new Vector3(i, 0, 0);
//            Vector3 end = new Vector3(i, spectrum[i] * 100f, 0);
//            Debug.DrawLine(start, end, Color.yellow);
//        }
    }

    void Update() {
        UpdateFlash();
        UpdateZoom();
    }

    private void Flash() {
        if(flashEnabled) {
            camera.backgroundColor = litColor;    
        }
    }

    private void UpdateFlash() {
        if(flashEnabled) {
            camera.backgroundColor = Color32.Lerp(camera.backgroundColor, darkColor, Time.deltaTime * flashSpeed);
        }
    }

    private void Zoom() {
        if(zoomEnabled) {
            camera.orthographicSize = camZoomSize;    
        }
    }

    private void UpdateZoom() {
        if(zoomEnabled) {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, camSize, Time.deltaTime * flashSpeed);
        }
    }
}
