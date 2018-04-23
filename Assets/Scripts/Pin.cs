using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public bool onFloor = false;
    private bool pointsTaken = false;
    private Vector3 initialPosition;
    private Vector3 zero;
    private Rigidbody rd;
    private int points = 10;
    private AudioSource audio;
 
    private void Awake()
    {
        GameManager.pins.Add(this);
        rd = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (transform.position.y < 0)
        {
            onFloor = true;
        }
        if (onFloor && !pointsTaken)
        {
            pointsTaken = true;
            GameManager.score += points;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pista")
        {
            onFloor = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bola")
        {
            audio.PlayDelayed(0);
        }
    }


    public bool IsOnFloor()
    {
        return onFloor;
    }

    public void Initialize()
    {
        initialPosition = transform.position;
    }

    public void ResetPin()
    {
        transform.position = initialPosition;
        rd.velocity = Vector3.zero;
        rd.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
