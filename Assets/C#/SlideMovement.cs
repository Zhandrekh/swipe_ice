using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMovement : MonoBehaviour {

    public float speed;
    public float timeToDeath;
    float timer;

    bool right;
    bool left;
    bool forward;
    bool backward;
    bool move;
    Vector3 direction;
    private void Start()
    {
        timer = timeToDeath;      
    }

    void Update () {

        HandleInput();
        Move();

        if (move)
        {
            timer -= Time.deltaTime;
        }

        transform.localScale =new Vector3(timer / timeToDeath, timer / timeToDeath, timer / timeToDeath);
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
        if(collision.gameObject.tag != "World")
        {
            move = false;
        }
    }
}
