using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

    public int initialNumLives = 3;

    public GameObject rocks;
    public GameObject rockTemplate;
    public Sprite[] largeRockSprites;

    public AudioClip levelUp;

    public ScoreDisplay scoreDisplay;
    public LivesRemainingDisplay livesDisplay;
    public static float sfxVolume = 0.25f;

    private int numLivesRemaining;
    private int levelNumber;
    private bool levelingUp = false;


    // Use this for initialization
    void Start() {
        if (FlexibleMusicManager.instance) {
            FlexibleMusicManager.instance.SetNewTrack(2);
            FlexibleMusicManager.instance.Play();
            FlexibleMusicManager.instance.repeat = true;
        } else {
            Debug.LogWarning("No music manager found.");
        }
        StartGame();
    }

    public void StartGame() {
        numLivesRemaining = initialNumLives;
        levelNumber = 1;
        StartLevel();
    }

    // Update is called once per frame
    void Update() {
        //count rocks remaining
        if (!levelingUp) {
            int count = rocks.transform.childCount;
            if (count == 0) {
                levelingUp = true;
                FindObjectOfType<Game>().LevelUp(1.5f);
            }
        }
    }

    public void Score(int amount) {
        scoreDisplay.Advance(amount);
    }

    public void LevelUp(float delay) {
        Score(100);
        Invoke("PlayLevelUp", delay / 2f);
        levelNumber++;
        Invoke("StartLevel", delay);
    }

    public void PlayLevelUp() {
        AudioSource.PlayClipAtPoint(levelUp, Camera.main.transform.position, 1f * sfxVolume);
    }

    public void StartLevel() {
        int count = 0;
        int desiredCount = 3;
        if (levelNumber > 2) {
            desiredCount = 4;
            if (levelNumber > 5) {
                desiredCount = 5;
                if (levelNumber > 10) {
                    desiredCount = 6;
                }
            }
        }
        count = rocks.transform.childCount;
        if (count < desiredCount) {
            SpawnRocks(desiredCount);
        }
        levelingUp = false;
        RestartLevel(0);
    }

    public void RestartLevel(float delay = 0) {
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
        livesDisplay.lives = numLivesRemaining;
    }

    public void GameOver() {
        SaveScore();
        FlexibleMusicManager.instance.Pause();
        LevelManager.instance.ChangeState(LevelManager.GameState.MENU, 0.5f);
    }

    public void SaveScore() {
        PlayerPrefs.SetInt("Score", scoreDisplay.score);
        int highScore = PlayerPrefs.GetInt("High Score");
        if (scoreDisplay.score > highScore)
            PlayerPrefs.SetInt("High Score", scoreDisplay.score);
    }

    public void SpawnRocks(int howmany) {
        //spread them out on a grid
        int gridX = (int)Mathf.Sqrt(howmany);
        int gridY = (int)Mathf.Sqrt(howmany);
        if (gridX * gridY < howmany) {
            gridX++;
        }
        if (gridX * gridY < howmany) {
            gridY++;
        }
        int count = 0;
        for (float x = -7; x <= 7; x += 14f / gridX) {
            for (float y = -4; y <= 4; y += 8f / gridY) {
                count++;
                if (count > howmany) {
                    break;
                }
                SpawnRock(x, y);
            }
        }
    }

    public void SpawnRock(float x, float y) {
        GameObject rock = Instantiate(rockTemplate);
        rock.transform.SetParent(rocks.transform);
        rock.transform.position = new Vector3(x, y, rock.transform.position.z);
        Rock r = rock.GetComponent<Rock>();
        r.velocity = new Vector2(x / 4f, y / 4f);
        r.angularVelocity = Random.Range(-180f, 180f);
        r.sizeTag = "Large";
        int index = Random.Range(0, largeRockSprites.Length);
        r.rockSprite = largeRockSprites[index];
    }
}
