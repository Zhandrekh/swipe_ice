using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    public GameObject player;
    
    SlideMovement playerMovement;
    [HideInInspector]
    public Quaternion startRot;

    public float speed;

    [Header("Transform Rotation")]
    public Transform leftRot;
    public Transform rightRot;
    public Transform frontRot;
    public Transform backRot;

    [HideInInspector]
    public bool wining = false;
        
    private void Awake()
    {
        startRot = transform.rotation;
    }
    private void Start()
    {
        playerMovement = player.GetComponent<SlideMovement>();
        
    }

    void Update ()
    {
        
        if (!playerMovement.move && !wining)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRot, Time.deltaTime * speed);
        }

		if(playerMovement.move && playerMovement.right)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, leftRot.rotation, Time.deltaTime * speed);
        }

        if (playerMovement.move && playerMovement.left)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rightRot.rotation, Time.deltaTime *speed);
        }

        if (playerMovement.move && playerMovement.forward)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, frontRot.rotation, Time.deltaTime * speed);
        }

        if (playerMovement.move && playerMovement.backward)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, backRot.rotation, Time.deltaTime * speed);
        }
    }
}
