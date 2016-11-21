using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    public ScoreDisplay scoreDisplay;
    public ScoreDisplay highScoreDisplay;

    // Use this for initialization
    void Start() {
        scoreDisplay.score = PlayerPrefs.GetInt("Score");
        highScoreDisplay.score = PlayerPrefs.GetInt("High Score");
        highScoreDisplay.prefixText = "High Score: ";
        if (FlexibleMusicManager.instance.CurrentTrackNumber() > 1) {
            FlexibleMusicManager.instance.SetNewTrack(1);
            FlexibleMusicManager.instance.Play();
        }
    }

    // Update is called once per frame
    void Update() {
        if (FlexibleMusicManager.instance.CurrentTrackNumber() > 0) {
            FlexibleMusicManager.instance.repeat = true;
        }

    }
}
