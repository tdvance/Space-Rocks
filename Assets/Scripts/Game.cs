using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public int initialNumLives = 3;

    private int numLivesRemaining;

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
        numLivesRemaining = initialNumLives;
        RestartLevel(0);
    }

    public void RestartLevel(float delay = 0) {
        //TODO fixed number of lives
        numLivesRemaining--;
        if (numLivesRemaining < 0) {
            GameOver();
        }
        Invoke("StartShip", delay);
    }

    public void StartShip() {
        Ship ship = FindObjectOfType<Ship>();
        if (!ship) {
            ship = Ship.SpawnNewShip();
        }
        ship.Reset();
    }

    public void GameOver() {
        FlexibleMusicManager.instance.Pause();
        LevelManager.instance.ChangeState(LevelManager.GameState.MENU, 0.5f);
    }
}
