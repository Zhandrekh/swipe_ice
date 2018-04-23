using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    public GameObject player;
    
    SlideMovement playerMovement;
    [HideInInspector]
    public Transform startRot;

    public float speed;

    [Header("Transform Rotation")]
    public Transform leftRot;
    public Transform rightRot;
    public Transform frontRot;
    public Transform backRot;

    private void Awake()
    {
        startRot = transform;
    }
    private void Start()
    {
        playerMovement = player.GetComponent<SlideMovement>();
        
    }

    void Update ()
    {
		if(playerMovement.move && playerMovement.right)
        {
            transform.rotation = Quaternion.Lerp(startRot.rotation, leftRot.rotation, Time.deltaTime * speed);
        }

        if (playerMovement.move && playerMovement.left)
        {
            transform.rotation = Quaternion.Lerp(startRot.rotation, rightRot.rotation, Time.deltaTime *speed);
        }

        if (playerMovement.move && playerMovement.forward)
        {
            transform.rotation = Quaternion.Lerp(startRot.rotation, frontRot.rotation, Time.deltaTime * speed);
        }

        if (playerMovement.move && playerMovement.backward)
        {
            transform.rotation = Quaternion.Lerp(startRot.rotation, backRot.rotation, Time.deltaTime * speed);
        }
    }
}
