using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesRemainingDisplay : MonoBehaviour {

    public GameObject life;
    public int lives = 3;

    private int lastLives = -1234;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (lives != lastLives) {
            UpdateLivesDisplay();
            lastLives = lives;
        }
    }

    void UpdateLivesDisplay() {
        foreach (Transform t in transform) {
            Destroy(t.gameObject);
        }
        if (lives > 0) {
            float width = 2/lives;
            float x = -width * lives / 2 + width / 2;
            for (int i = 0; i < lives; i++) {
                Instantiate(life, transform);
                life.transform.position = new Vector3(x, 0, 0) + transform.position;
                x += width;
            }
        }

    }
}
