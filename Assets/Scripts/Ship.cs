using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Ship : MonoBehaviour {
    public float accelerationFactor = 1f;
    public float turnSpeed = 1f;

    public float frontDistance = 0.456f;
    public GameObject missilePrefab;


    private float accelerate;
    private float turn;
    private Rigidbody2D rb;

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
        rb.AddForce(accelerate * accelerationFactor*transform.up);
    }

    void Fire() {
        Instantiate(missilePrefab, transform.position + transform.up * frontDistance, transform.localRotation);
    }
}
