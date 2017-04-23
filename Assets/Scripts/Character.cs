using UnityEngine;

public class Character : MonoBehaviour {

    public GameController gameController;

    public void Kill() {
        gameController.OnKill();
        Destroy(gameObject);
    }
}
