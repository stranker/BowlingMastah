using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bola : MonoBehaviour {

    private Vector3 initialBallPosition;
    public float horizontalVelocity;
    private float horizontalMovement;
    public float force;
    private float MAX_FORCE = 7000;
    private Rigidbody rd;
    [SerializeField]
    private GameObject pista;
    [SerializeField]
    private Image forceBar;
    private float pistaSizeZ;
    private float sizeBall;
    [SerializeField]
    private bool throwed;
    [SerializeField]
    private Text remainingShoot;
    [SerializeField]
    private Image arrowForce;
    private AudioSource audio;

    // Use this for initialization

    void Start ()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        if (!throwed)
        {
            Vector3 clampedPosition = transform.position;
            horizontalMovement = Input.GetAxis("Horizontal");
            transform.Translate(0, 0, horizontalMovement * Time.deltaTime * horizontalVelocity);
            clampedPosition.z = Mathf.Clamp(transform.position.z, -pistaSizeZ / 2 + sizeBall / 2, pistaSizeZ / 2 - sizeBall / 2);
            transform.position = clampedPosition;
            force += Input.GetAxis("Vertical") * 50;
            force = Mathf.Clamp(force, 0, MAX_FORCE);
            arrowForce.GetComponent<CanvasGroup>().alpha = 1;
            arrowForce.GetComponent<CanvasGroup>().interactable = true;
            arrowForce.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else {
            arrowForce.GetComponent<CanvasGroup>().alpha = 0;
            arrowForce.GetComponent<CanvasGroup>().interactable = false;
            arrowForce.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        remainingShoot.text = "Remaining shoots " + (GameManager.RemainingShoots());
        forceBar.fillAmount = force / MAX_FORCE;
        if (Input.GetKeyDown(KeyCode.Space) && !throwed && GameManager.remainingShoots > 0)
        {
            rd.AddForce(-transform.right * force);
            GameManager.remainingShoots--;
            throwed = true;
            audio.PlayDelayed(0);
        }

        if (transform.position.y < 0.8)
            audio.mute = true;
        else
            audio.mute = false;
    }

    public void Initialize()
    {
        rd = GetComponent<Rigidbody>();
        pistaSizeZ = pista.GetComponent<Renderer>().bounds.size.z;
        sizeBall = GetComponent<Renderer>().bounds.size.z;
        horizontalVelocity = 5;
        throwed = false;
        initialBallPosition = transform.position;
        force = 0;
        audio = GetComponent<AudioSource>();
    }

    public void ResetBall()
    {
        force = 0;
        transform.position = initialBallPosition;
        throwed = false;
        rd.velocity = Vector3.zero;
        rd.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public bool IsThrowed()
    {
        return throwed;
    }


}
