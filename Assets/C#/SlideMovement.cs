using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMovement : MonoBehaviour {

    public float speed;
    public float movement;
    float damages = 1;
    float triggerTimer = 2;

    public ParticleSystem trail;
    public ParticleSystem kaboom;

    public GameObject manager;
    bool right;
    bool left;
    bool forward;
    bool backward;
    public bool move;
    Vector3 direction;

    private void Awake()
    {
        movement = movement + 2;
    }

    private void Start()
    {
        damages = damages / movement;
    }

    void Update () {
        
        HandleInput();
        Move();

        if (move)
        {
            trail.Play();
        }
        else
        {
            trail.Stop();
        }

        
    }

    void HandleInput()
    {
        if (Input.GetAxis("Horizontal") > 0 && !move)
        {
            right = true;
            left = false;
            forward = false;
            backward = false;
            move = true;
        }

        if (Input.GetAxis("Horizontal") < 0 && !move)
        {
            right = false;
            left = true;
            forward = false;
            backward = false;
            move = true;
        }

        if (Input.GetAxis("Vertical") > 0 && !move)
        {
            right = false;
            left = false;
            forward = true;
            backward = false;
            move = true;
        }

        if (Input.GetAxis("Vertical") < 0 && !move)
        {
            right = false;
            left = false;
            forward = false;
            backward = true;
            move = true;
        }
    }

    void Move()
    {
        if (right)
            direction = Vector3.right;

        if (left)
            direction = Vector3.left;

        if (forward)
            direction = Vector3.forward;

        if (backward)
            direction = Vector3.back;

        if (move)
            transform.Translate(direction * Time.deltaTime *speed, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "World")
        {
            move = false;
            transform.localScale -= new Vector3(damages, damages, damages);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            
            triggerTimer -= Time.deltaTime;

            if (triggerTimer <= 0f)
                manager.GetComponent<PartyManager>().Win();
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        triggerTimer = 2;

        if (other.CompareTag("Switch"))
        {
            right = false;
            left = false;
            forward = false;
            backward = false;
            direction = other.GetComponent<ChangeDirection>().newDirection;
        }
    }
}
