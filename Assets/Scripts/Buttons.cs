using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {
    public void SubmitStart() {
        FlexibleMusicManager.instance.Pause();
        LevelManager.instance.ChangeState(LevelManager.GameState.GAME, 0.5f);
    }

    public void SubmitQuit() {
        Application.Quit();
    }

    public void SubmitOptions() {
        LevelManager.instance.ChangeState(LevelManager.GameState.OPTIONS, 0f);
    }

    public void SubmitResumeMenu() {
        LevelManager.instance.ChangeState(LevelManager.GameState.MENU, 0f);
    }

    public void SubmitExitGame() {
        //TODO call end of game method of game class
        Time.timeScale = 1f;
        FlexibleMusicManager.instance.Pause();
        LevelManager.instance.ChangeState(LevelManager.GameState.MENU, 0.5f);
    }

    public void SubmitOptionsFromGame() {
        Time.timeScale = 0f;
        FindObjectOfType<Canvas>().enabled = false;
        LevelManager.instance.ChangeState(LevelManager.GameState.OPTIONS_FROM_GAME, 0f);
    }

    public void SubmitResumeGame() {
        LevelManager.instance.ChangeState(LevelManager.GameState.GAME, 0f);
        Time.timeScale = 1f;
        FindObjectOfType<Canvas>().enabled = true;
    }

}
