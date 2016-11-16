using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
    public float speed = 50;
    public float timeToLive = 1.5f;


    private Rigidbody2D rb;
    private float startTime;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update () {
	    if(Time.time-startTime > timeToLive) {
            Destroy(gameObject);
        }
	}
}
