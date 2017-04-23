using UnityEngine;

public class EqualizerBar : MonoBehaviour {

    public EqualizerLit[] lits;
    public Color[] litColor;
    public Color darkColor;
    public float fastSpeed;
    public float maxValue;

    public void SetLit(float spectrumValue) {
        float percent = spectrumValue / maxValue;
        int litIndex = 0;
        for(litIndex = 0; litIndex < lits.Length; litIndex++) {
            if((float)litIndex / lits.Length <= percent) {
                lits[litIndex].color = GetLitColor(litIndex);
                lits[litIndex].speed = fastSpeed;
            } else {
                break;
            }
        }
    }

    private Color GetLitColor(int i) {
        int colorIndex = Mathf.FloorToInt((float)i / litColor.Length);
        return litColor[colorIndex];
    }
}
