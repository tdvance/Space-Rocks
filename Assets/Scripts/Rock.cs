using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {
    public Vector2 velocity;
    public float angularVelocity;
    public float maxVelocity = 3f;
    public Sprite rockSprite;
    public string sizeTag;
    public GameObject rockTemplate;
    public Sprite[] largeRockSprites;
    public Sprite[] mediumRockSprites;
    public Sprite[] smallRockSprites;
    public Sprite[] tinyRockSprites;
    public AudioClip explode;
    public AudioClip collide;

    Rigidbody2D rb;
    AudioSource audioSource;
    Game game;

    // Use this for initialization
    void Start() {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        game = FindObjectOfType<Game>();

        rb.velocity = velocity;
        if (rb.velocity.magnitude > maxVelocity) {
            rb.velocity *= maxVelocity / rb.velocity.magnitude;
        }
        rb.angularVelocity = angularVelocity;
        if (rockSprite) {
            GetComponent<SpriteRenderer>().sprite = rockSprite;
        }
        //Debug.Log("Rock " + name + " with tag " + gameObject.tag + " changed to " + sizeTag);
        gameObject.tag = sizeTag;

        //Kludge: reset the polygon collider
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    void FixedUpdate() {
        if (rb.velocity.magnitude > maxVelocity) {
            rb.velocity *= Mathf.Sqrt(maxVelocity / rb.velocity.magnitude);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Missile") {
            AudioSource.PlayClipAtPoint(explode, Camera.main.transform.position);            
            Destroy(collision.gameObject);
            BreakApart();
        } else {
            if (audioSource) {
                audioSource.clip = collide;
                audioSource.volume = .05f;
                audioSource.Play();
            }
        }
    }

    void BreakApart() {
        if (sizeTag == "Large") {
            game.Score(50);
            for (int i = 0; i < 4; i++) {
                SpawnRock(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            }
        } else if (sizeTag == "Medium") {
            game.Score(20);
            for (int i = 0; i < 2; i++) {
                SpawnRock(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            }
        } else if (sizeTag == "Small") {
            game.Score(10);
            SpawnRock(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        } else {
            game.Score(5);
            //TODO spawn smoke          
        }
        Destroy(gameObject);
    }

    void SpawnRock(float xVel, float yVel) {
        GameObject rock = Instantiate(rockTemplate);
        rock.transform.SetParent(transform.parent);
        rock.transform.position = transform.position;
        Rock r = rock.GetComponent<Rock>();
        r.velocity = rb.velocity + new Vector2(xVel, yVel);
        r.angularVelocity = rb.angularVelocity * 3f;
        if (sizeTag == "Large") {
            int index = Random.Range(0, 3);
            switch (index) {
                case 0:
                    index = Random.Range(0, mediumRockSprites.Length);
                    r.rockSprite = mediumRockSprites[index];
                    r.sizeTag = "Medium";
                    break;
                case 1:
                    index = Random.Range(0, smallRockSprites.Length);
                    r.rockSprite = smallRockSprites[index];
                    r.sizeTag = "Small";
                    break;
                default:
                    index = Random.Range(0, tinyRockSprites.Length);
                    r.rockSprite = tinyRockSprites[index];
                    r.sizeTag = "Tiny";
                    break;
            }
        } else if (sizeTag == "Medium") {
            int index = Random.Range(0, 2);
            if (index == 0) {
                index = Random.Range(0, smallRockSprites.Length);
                r.rockSprite = smallRockSprites[index];
                r.sizeTag = "Small";
            } else {
                index = Random.Range(0, tinyRockSprites.Length);
                r.rockSprite = tinyRockSprites[index];
                r.sizeTag = "Tiny";
            }
        } else if (sizeTag == "Small") {
            int index = Random.Range(0, tinyRockSprites.Length);
            r.rockSprite = tinyRockSprites[index];
            r.sizeTag = "Tiny";
        }
    }

}
