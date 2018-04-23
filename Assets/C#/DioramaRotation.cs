using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DioramaRotation : MonoBehaviour {

    public float speed;
    float _speed;
    public Vector3 axis;

	void Update ()
    {
        _speed += Time.deltaTime * speed;
        transform.rotation =  Quaternion.AngleAxis(_speed,axis);
	}
}
