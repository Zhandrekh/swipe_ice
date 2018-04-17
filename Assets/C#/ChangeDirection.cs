using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirection : MonoBehaviour {

    public bool right;
    public bool left;
    public bool forward;
    public bool backward;


    [HideInInspector]
    public Vector3 newDirection;

    private void Start()
    {

        if (right)
            newDirection = Vector3.right;

        if (left)
            newDirection = Vector3.left;

        if (forward)
            newDirection = Vector3.forward;

        if (backward)
            newDirection = Vector3.back;
    }

}
