using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public enum GameState {
        INIT,
        MENU,
        OPTIONS,
        GAME,
        OPTIONS_FROM_GAME,
        GAME_OVER,
    }

    public GameState currentState = GameState.INIT;

    public string initScene = "_Init";
    public string menuScene = "MainMenu";
    public string optionsScene = "Options";
    public string startGameScene = "Game";
    public string optionsFromGameScene = "OptionsInGame";
    public string gameOverScene = "GameOver";
    public float initSceneTime = 2.5f;

    private GameState realCurrentState = GameState.INIT;
    private GameState nextState;
    private string[] stateScenes;


    public void ChangeState(GameState state, float delay = 0f) {
        CancelInvoke();
        nextState = state;
        if (delay != 0) {
            Invoke("NextState", delay);
        } else {
            NextState();
        }
    }

    void NextState() {
        currentState = nextState;
    }

    void Start() {
        stateScenes = new string[6];
        stateScenes[(int)GameState.INIT] = initScene;
        stateScenes[(int)GameState.MENU] = menuScene;
        stateScenes[(int)GameState.OPTIONS] = optionsScene;
        stateScenes[(int)GameState.GAME] = startGameScene;
        stateScenes[(int)GameState.OPTIONS_FROM_GAME] = optionsFromGameScene;
        stateScenes[(int)GameState.GAME_OVER] = gameOverScene;
        if (initSceneTime != 0) {
            ChangeState(GameState.MENU, initSceneTime);
        }
    }

    void Update() {
        if (realCurrentState != currentState) {
            UpdateState();
        }
    }

    void UpdateState() {
        if (realCurrentState == GameState.GAME && currentState == GameState.OPTIONS_FROM_GAME) {
            Debug.Log("Adding scene: " + stateScenes[(int)currentState]);
            SceneManager.LoadScene(stateScenes[(int)currentState], LoadSceneMode.Additive);
        } else if (realCurrentState == GameState.OPTIONS_FROM_GAME && currentState == GameState.GAME) {
            Debug.Log("Removing scene: " + stateScenes[(int)realCurrentState]);
            SceneManager.UnloadScene(stateScenes[(int)realCurrentState]);
        } else {
            Debug.Log("Loading scene: " + stateScenes[(int)currentState]);
            SceneManager.LoadScene(stateScenes[(int)currentState]);
        }
        realCurrentState = currentState;
    }

    #region Singleton
    private static LevelManager _instance;

    public static LevelManager instance {
        get {
            if (_instance == null) {//in case not awake yet
                _instance = FindObjectOfType<LevelManager>();
            }
            return _instance;
        }
    }

    void Awake() {
        if (_instance != null && _instance != this) {
            Debug.LogError("Duplicate singleton " + this.gameObject + " created; destroying it now");
            Destroy(this.gameObject);
        }

        if (_instance != this) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }
    #endregion

}
