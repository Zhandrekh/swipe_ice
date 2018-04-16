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
    float timerStart = 4;
    float timer = 4;

	void Start () {
        player.transform.position = startPos.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (count)
        {
            timer -= Time.deltaTime;
        }

		if (player.GetComponent<SlideMovement>().timer <= 0.01)
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
            player.GetComponent<SlideMovement>().timer = player.GetComponent<SlideMovement>().timeToDeath;
            loseScreen.gameObject.SetActive(false);
            timer = timerStart;
        }
        
    }

    public void Win()
    {
        winScreen.gameObject.SetActive(true);
    }
}
