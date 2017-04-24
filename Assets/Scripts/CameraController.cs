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
    private Camera camera;
    public AudioProcessor processor;

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
        camera.backgroundColor = Color32.Lerp(camera.backgroundColor, darkColor, Time.deltaTime * flashSpeed);
    }

    private void Zoom() {
        if(zoomEnabled) {
            camera.orthographicSize = camZoomSize;    
        }
    }

    private void UpdateZoom() {
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, camSize, Time.deltaTime * flashSpeed);
    }

    public void FlashScreen() {
        CameraFade.StartAlphaFade(Color.white, true, 1f, 0f);    
    }

}
