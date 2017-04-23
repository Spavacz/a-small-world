using UnityEngine;

public class Equalizer : MonoBehaviour {

    public AudioProcessor audioProcessor;
    public EqualizerBar[] bars;

    void Start() {
        audioProcessor.onSpectrum.AddListener(OnSpectrum);
    }

    private void OnSpectrum(float[] spectrum) {
        for(int i = 0; i < spectrum.Length; ++i) {
            bars[i].SetLit(spectrum[i]);
        }
    }
}
