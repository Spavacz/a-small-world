using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour {

    private bool isReady;

    void Start() {
        CameraFade.StartAlphaFade(Color.black, true, 5f, 0f, () => {
            isReady = true;
        });
    }

    void Update() {
        if(isReady) {
            if(Input.GetButtonDown("Quit")) {
                Application.Quit();
            } else if(Input.anyKeyDown) {
                CameraFade.StartAlphaFade(Color.black, false, 2f, 0f, () => {
                    SceneManager.LoadScene("main");
                });
            }
        }
    }
}
