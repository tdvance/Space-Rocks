using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Ship : MonoBehaviour {
    public float accelerationFactor = 1f;
    public float turnSpeed = 1f;

    public float frontDistance = 0.456f;
    public GameObject missilePrefab;

    public float initialHealth = 4;
    public float bigDamage = 2;
    public float smallDamage = 1;

    public GameObject shipPrefab;

    public GameObject smokePrefab;

    public AudioClip explodeBig;
    public AudioClip explodeSmall;
    public AudioClip explodeDie;


    private float accelerate;
    private float turn;
    private Rigidbody2D rb;
    private float health = 4;

    private static GameObject shipPrefabStatic;

    // Use this for initialization
    void Start() {
        if (shipPrefab && !shipPrefabStatic) {
            shipPrefabStatic = shipPrefab;
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        turn = CrossPlatformInputManager.GetAxis("Horizontal");
        accelerate = CrossPlatformInputManager.GetAxis("Vertical");
        if (CrossPlatformInputManager.GetButtonDown("Fire1")) {
            Fire();
        }
    }

    void FixedUpdate() {
        rb.angularVelocity = -turn * turnSpeed;
        rb.AddForce(accelerate * accelerationFactor * transform.up);
    }

    void Fire() {
        Instantiate(missilePrefab, transform.position + transform.up * frontDistance,
            transform.localRotation);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Missile"
            || collision.gameObject.tag == "Large"
            || collision.gameObject.tag == "Medium") {
            AudioSource.PlayClipAtPoint(explodeBig, Camera.main.transform.position, .2f);
            Damage(bigDamage);
        } else if (collision.gameObject.tag == "Small"
             || collision.gameObject.tag == "Tiny") {
            AudioSource.PlayClipAtPoint(explodeSmall, Camera.main.transform.position, .1f);
            Damage(smallDamage);
        }
    }

    void Damage(float amount) {
        health -= amount;
        //TODO show damage on sprite
        if (health < 0) {
            Die();
        }
    }


    void Die() {
        GameObject s = Instantiate(smokePrefab, transform.position, transform.localRotation) as GameObject;
        s.GetComponent<ParticleSystem>().startColor = new Color(1f, .5f, 0, .1f);
        Destroy(s, 5f);
        AudioSource.PlayClipAtPoint(explodeDie, Camera.main.transform.position, 1f);
        Game game = FindObjectOfType<Game>();
        game.RestartLevel(1.5f);
        Destroy(gameObject);
    }

    public void Reset() {
        health = initialHealth;
    }

    public static Ship SpawnNewShip() {
        GameObject obj = GameObject.Instantiate(Ship.shipPrefabStatic);
        return obj.GetComponent<Ship>();
    }
}