using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

	// Use this for initialization
	void Awake() {
		/*GameObject[] obs = GameObject.FindGameObjectsWithTag ("music");
		if (obs.Length > 1)
			Destroy (this.gameObject);*/

		DontDestroyOnLoad (this.gameObject);
	}

}