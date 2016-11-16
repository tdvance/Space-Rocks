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
        LevelManager.instance.ChangeState(LevelManager.GameState.OPTIONS, 0.5f);
    }

    public void SubmitResumeMenu() {
        LevelManager.instance.ChangeState(LevelManager.GameState.MENU, 0.5f);
    }

    public void SubmitExitGame() {
        //TODO call end of game method of game class
        FlexibleMusicManager.instance.Pause();
        LevelManager.instance.ChangeState(LevelManager.GameState.MENU, 0.5f);
    }

    public void SubmitOptionsFromGame() {
        //TODO pause game, then load options menu from game additively
    }

    public void SubmitResumeGame() {
        //TODO destroy options menu from game and resume game from pause
    }

}
