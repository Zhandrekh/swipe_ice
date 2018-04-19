using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneSelection))]
public class PartyManager : MonoBehaviour {

    [Header("Level structs")]
    public GameObject player;
    public Transform startPos;
    public GameObject tryMarker;
    public Transform trySlot;
    public float slotSpacing;
    public List<GameObject> slots;
    public GameObject finish;
    public int currentSceneIndex;
    
    [Header("Screens")]
    public Canvas loseScreen;
    public Canvas winScreen;
    
    bool count = false;
    float timerStart = 2;
    float timer = 2;

	void Start () {
        InitGame();
	}
    
	void Update () {

        if (count)
        {
            timer -= Time.deltaTime;
        }

		if (slots.Count == 0)
        {
            Lose();
        }

        if (Input.GetButton("Reset"))
        {
            InitGame();
        }
	}

    public void Lose()
    {
        
        player.GetComponent<SlideMovement>().move = false;
        
        count = true;
        loseScreen.gameObject.SetActive(true);
        if(timer <= 0)
        {
            InitGame();   
        }
        
    }

    public void Win()
    {
        winScreen.gameObject.SetActive(true);
        count = true;

        if (timer <= 0)
        {
            GetComponent<SceneSelection>().NextLevel(currentSceneIndex);
        }
    }

    void InitGame()
    {
        count = false;
        player.transform.position = startPos.position;
        player.transform.localScale = new Vector3(1, 1, 1);
        loseScreen.gameObject.SetActive(false);
        timer = timerStart;
        
        if (slots.Count != 0)
        {
            int listCount = slots.Count;
            for (int i = 0; i < listCount; i++)
            {
                GameObject current = slots[0];
                slots.Remove(current);
                Destroy(current);
            }
        }       
        
        slots = new List<GameObject>();
        for (int i = 0; i < player.GetComponent<SlideMovement>().movement; i++)
        {
            GameObject go = Instantiate(tryMarker);
            go.transform.position = new Vector3(trySlot.position.x + slotSpacing * i, trySlot.position.y, trySlot.position.z);
            slots.Add(go);

        }
    }

    public void CountTry()
    {
        
        GameObject current = slots[slots.Count-1];
        slots.Remove(current);
        Destroy(current);
    }
}
