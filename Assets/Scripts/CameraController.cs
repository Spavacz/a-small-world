using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    public float flashSpeed;

    public bool flashEnabled;
    public Color litColor;
    public Color darkColor;

    public bool zoomEnabled;
    public float camSize;
    public float camZoomSize;

    private Color color;
    private Camera cam;
    public AudioProcessor processor;

    void Awake() {
        cam = GetComponent<Camera>();
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
    }

    void Update() {
        UpdateFlash();
        UpdateZoom();
    }

    private void Flash() {
        if(flashEnabled) {
            cam.backgroundColor = litColor;    
        }
    }

    private void UpdateFlash() {
        cam.backgroundColor = Color32.Lerp(cam.backgroundColor, darkColor, Time.deltaTime * flashSpeed);
    }

    private void Zoom() {
        if(zoomEnabled) {
            cam.orthographicSize = camZoomSize;    
        }
    }

    private void UpdateZoom() {
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, camSize, Time.deltaTime * flashSpeed);
    }

    public void FlashScreen() {
        CameraFade.StartAlphaFade(Color.white, true, 1f, 0f);    
    }

}
