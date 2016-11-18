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

    private float accelerate;
    private float turn;
    private Rigidbody2D rb;
    private float health = 4;

    // Use this for initialization
    void Start() {
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
            Damage(bigDamage);
        } else if (collision.gameObject.tag == "Small"
             || collision.gameObject.tag == "Tiny") {
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
        StartGame game = FindObjectOfType<StartGame>();
        game.RestartLevel(1.5f);
        Destroy(gameObject);
    }

    public void Reset() {
        health = initialHealth;
    }

    public static Ship SpawnNewShip() {
        Debug.Log("Spawn New Ship");
        return null;//TODO
    }
}