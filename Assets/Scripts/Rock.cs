using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {
    public Vector2 velocity;

    public float angularVelocity;

    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
        rb.angularVelocity = angularVelocity;
	}
	
	// Update is called once per frame
	void Update () {
	    

	}

}
