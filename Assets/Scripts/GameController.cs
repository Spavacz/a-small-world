using UnityEngine;

public class GameController : MonoBehaviour {
    public Camera cam;

    public GameObject worldPrefab;
    public GameObject characterPrefab;

    public float rotationSpeed = 2f;
    public float rotationStopTime = 0.5f;
    private float currentRotationSpeed = 0;
    private float rotationVelocity = 0;

    private GameObject world;

    void Start() {
        world = Instantiate(worldPrefab);
    }

    void Update() {
        if(Input.GetButtonDown("Fire2")) {
            SpawnCharacter();
        }
        UpdateWorldRotationSpeed();
    }

    void FixedUpdate() {
        ApplyRotationSpeed();
    }

    private void UpdateWorldRotationSpeed() {
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition); 
        float rotation = 0;
        if(Input.GetButton("Fire1")) {
            float h = rotationSpeed * Input.GetAxis("Mouse X");
            float v = rotationSpeed * Input.GetAxis("Mouse Y");
            float x = mouseWorldPos.x;
            float y = mouseWorldPos.y;

            rotation += x > 0 ? v : -v;
            rotation += y > 0 ? -h : h;
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
            world.transform.Rotate(0, 0, currentRotationSpeed);    
        }
    }

    private void SpawnCharacter() {
        Instantiate(characterPrefab);
    }
}
