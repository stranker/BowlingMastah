using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    [SerializeField]
    private Bola ball;
    private Vector3 initialPosition;
	// Use this for initialization
	void Start () {
        initialPosition = transform.position;
	}



    // Update is called once per frame
    void Update () {
        if (ball.IsThrowed())
            FollowBall();
        else
            transform.position = initialPosition;
    }

    private void FollowBall()
    {

        if (transform.position.x > -15)
        {
            Vector3 newPos = Vector3.zero;
            newPos.y = ball.transform.position.y + 9;
            newPos.x = ball.transform.position.x + 10;
            transform.position = newPos;
        }
        
    }
}
