using UnityEngine;
using EZObjectPools;

public class Character : MonoBehaviour {

    public Vector3 growScale;
    public float effectSpeed = 2f;

    public EZObjectPool killObjectPool;
    public GameController gameController;
    public AudioProcessor audioProcessor;

    void Start() {
        audioProcessor.onSpectrum.AddListener(OnSpectrum);
    }

    void Update() { 
        if(transform.localScale != Vector3.one) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * effectSpeed);
        }
    }

    public void Kill() {
        killObjectPool.TryGetNextObject(transform.position, transform.rotation);
        gameController.OnKill();
        gameObject.SetActive(false);
    }

    private void OnSpectrum(float[] spectrum) {
        if(spectrum[5] > 0.04f) {
            Grow();
        }
    }

    private void Grow() {
        transform.localScale = growScale;
    }
}
