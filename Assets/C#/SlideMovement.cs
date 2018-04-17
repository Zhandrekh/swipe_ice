using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMovement : MonoBehaviour {

    public float speed;
    public float timeToDeath;
    public float timer;
    float triggerTimer = 2;

    public GameObject manager;
    bool right;
    bool left;
    bool forward;
    bool backward;
    public bool move;
    Vector3 direction;
    private void Start()
    {
        timer = timeToDeath;      
    }

    void Update () {
        Debug.Log(transform.position);
        HandleInput();
        Move();

        if (move)
        {
            timer -= Time.deltaTime;
        }

        //transform.localScale =new Vector3(timer / timeToDeath, timer / timeToDeath, timer / timeToDeath);
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
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
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
            Debug.Log("Hey");
            right = false;
            left = false;
            forward = false;
            backward = false;
            direction = other.GetComponent<ChangeDirection>().newDirection;
        }
    }
}
