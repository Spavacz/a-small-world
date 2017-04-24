using UnityEngine;
using UnityEngine.SceneManagement;
using EZObjectPools;

public class GameController : MonoBehaviour {
    public Camera cam;

    public GameObject world;
    public GameObject characterPrefab;

    public CameraController cameraController;
    public AudioSource musicAudioSource;
    public AudioSource gameOverAudioSource;
    public EZObjectPool killEffectObjectPool;
    public EZObjectPool characterObjectPool;

    public float mouseSensitivity = 10f;
    public float gravityForce = 9.81f;
    public float rotationSpeed = 2f;
    public float rotationStopTime = 0.5f;
    private float gravity;
    private float currentRotationSpeed = 0;
    private float rotationVelocity = 0;
    private int alive;

    void Start() {
        CameraFade.StartAlphaFade(Color.black, true, 7f, 2f, () => {
            gravity = gravityForce;
            UpdateGravity();
        });
        Physics2D.gravity = Vector2.zero;
        SpawnCharacter();
    }

    void Update() {
        if(Input.GetButtonUp("Quit")) {
            Application.Quit();
        }
        UpdateWorldRotationSpeed();
    }

    void FixedUpdate() {
        ApplyRotationSpeed();
    }

    private void UpdateWorldRotationSpeed() {
        float rotation = 0;
        if(Input.GetButton("Rotate")) {
            float x = Input.mousePosition.x - Screen.width / 2;
            float y = Input.mousePosition.y - Screen.height / 2;
            float h = rotationSpeed * Input.GetAxis("Mouse X") * mouseSensitivity;
            float v = rotationSpeed * Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotation += x > 0 ? -v : v;
            rotation += y > 0 ? h : -h;
            rotation = Mathf.Clamp(rotation, -rotationSpeed, rotationSpeed);
        } else if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            float h = rotationSpeed * Input.GetAxis("Horizontal");
            float v = rotationSpeed * Input.GetAxis("Vertical");
            rotation = v - h;
            rotation = Mathf.Clamp(rotation, -rotationSpeed, rotationSpeed);
        } else if(currentRotationSpeed != 0) {
            rotation = 0;
        }

        if(currentRotationSpeed != rotation) {
            currentRotationSpeed = Mathf.SmoothDamp(currentRotationSpeed, rotation, ref rotationVelocity, rotationStopTime);    
        }
    }

    private void ApplyRotationSpeed() {
        if(currentRotationSpeed != 0) {
            cameraController.transform.Rotate(0, 0, currentRotationSpeed);    
            UpdateGravity();
        }
    }

    private void UpdateGravity() {
        Quaternion cameraRotation = cameraController.transform.rotation;
        Physics2D.gravity = Trigonometry.DegreeToVector2(cameraRotation.eulerAngles.z + 90f) * -gravity;
    }

    private void SpawnCharacter() {
        CreateCharacter(new Vector3(0, 0, -3f), Quaternion.identity);
    }

    public void CloneCharacter(GameObject character) {
        CreateCharacter(character.transform.position, character.transform.rotation);
    }

    private void CreateCharacter(Vector3 position, Quaternion rotation) {
        alive++;
        GameObject created;
        characterObjectPool.TryGetNextObject(position, rotation, out created);
        SetupCharacter(created);
    }

    private void SetupCharacter(GameObject characterGameObject) {
        Character character = characterGameObject.GetComponent<Character>();
        character.killObjectPool = killEffectObjectPool;
        character.gameController = this;
        character.audioProcessor = cameraController.processor;
    }

    public void OnKill() {
        alive--;
        if(alive <= 0) {
            GameOver();
        }
    }

    private void GameOver() {
        musicAudioSource.Stop();
        gameOverAudioSource.Play();
        CameraFade.StartAlphaFade(Color.black, false, 1f, 1f, () => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
}
