using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMovement : MonoBehaviour {

    [Header("Cube Stats")]
    public float speed;
    public AnimationCurve acceleration;
    float activeTime;
    public float movement;
    float _movement;
    float damages = 1;
    float triggerTimer = 2;

    [Header("Raycast Mask")]
    public LayerMask layerMask = -1;

    [Header("Particles")]
    public ParticleSystem trail;
    public ParticleSystem kaboom;

    [Header("Game Manager")]
    public GameObject manager;

    [HideInInspector]
    public bool right;
    [HideInInspector]
    public bool left;
    [HideInInspector]
    public bool forward;
    [HideInInspector]
    public bool backward;
    [HideInInspector]
    public bool move;

    bool won;
    Vector3 direction;

    private void Awake()
    {
        _movement = movement + 2;
    }

    private void Start()
    {
        damages = damages / _movement;
    }

    void Update () {

        
        HandleInput();
        Move();
        
    }
    
    void HandleInput()
    {
        if (Input.GetAxis("Horizontal") > 0 && !move)
        {
            
            if (!(Physics.Raycast(transform.position, transform.right, 0.75F, layerMask)) )
            {
                right = true;
                left = false;
                forward = false;
                backward = false;
                move = true;
                activeTime = Time.time;
            }
        }

        if (Input.GetAxis("Horizontal") < 0 && !move)
        {
            if (!(Physics.Raycast(transform.position, -transform.right, 0.75F, layerMask)))
            {
                right = false;
                left = true;
                forward = false;
                backward = false;
                move = true;
                activeTime = Time.time;
            }            
        }

        if (Input.GetAxis("Vertical") > 0 && !move)
        {
            if (!(Physics.Raycast(transform.position, transform.forward, 0.75F, layerMask)))
            {
                right = false;
                left = false;
                forward = true;
                backward = false;
                move = true;
                activeTime = Time.time;
            }            
        }

        if (Input.GetAxis("Vertical") < 0 && !move)
        {
            if (!(Physics.Raycast(transform.position, -transform.forward, 0.75F, layerMask)))
            {
                right = false;
                left = false;
                forward = false;
                backward = true;
                move = true;
                activeTime = Time.time;
            }               
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
        {
            manager.GetComponent<PartyManager>().SlideSound();
            transform.Translate(direction * Time.deltaTime * speed * acceleration.Evaluate(Time.time - activeTime), Space.World);
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        move = false;
        kaboom.Play();
        manager.GetComponent<PartyManager>().StopSlideSound();
        if (collision.gameObject.tag == "World")
        {
            transform.localScale -= new Vector3(damages, damages, damages);
            manager.GetComponent<PartyManager>().CountTry();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            
            triggerTimer -= Time.deltaTime;

            if (triggerTimer <= 0f && !won)
            {
                manager.GetComponent<PartyManager>().Win();
                won = true;
            }
                
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
