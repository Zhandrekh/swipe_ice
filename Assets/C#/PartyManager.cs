using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {

    public GameObject player;
    public Transform startPos;
    public GameObject finish;
    
    public Canvas loseScreen;
    public Canvas winScreen;

    bool count = false;
    float timerStart = 2;
    float timer = 2;

	void Start () {
        player.transform.position = startPos.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (count)
        {
            timer -= Time.deltaTime;
        }

		if (player.transform.localScale.x < 0.05f)
        {
            Lose();
        }
	}

    public void Lose()
    {
        
        player.GetComponent<SlideMovement>().move = false;
        
        count = true;
        loseScreen.gameObject.SetActive(true);
        if(timer <= 0)
        {
            count = false;          
            player.transform.position = startPos.position;
            player.transform.localScale = new Vector3(1, 1, 1);
            loseScreen.gameObject.SetActive(false);
            timer = timerStart;
        }
        
    }

    public void Win()
    {
        winScreen.gameObject.SetActive(true);
    }
}
