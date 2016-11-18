using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

    // Use this for initialization
    void Start() {
        if (FlexibleMusicManager.instance) {
            FlexibleMusicManager.instance.SetNewTrack(2);
            FlexibleMusicManager.instance.Play();
            FlexibleMusicManager.instance.repeat = true;
        } else {
            Debug.LogWarning("No music manager found.");
        }
        StartLevel();
    }

    // Update is called once per frame
    void Update() {

    }

    public void StartLevel() {
        RestartLevel(0);
    }

    public void RestartLevel(float delay = 0) {
        //TODO fixed number of lives
        Invoke("StartShip", delay);
    }

    public void StartShip() {
        Ship ship = FindObjectOfType<Ship>();
        if (!ship) {
            ship = Ship.SpawnNewShip();
        }
        ship.Reset();
    }


}
