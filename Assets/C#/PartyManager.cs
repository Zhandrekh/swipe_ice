using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SceneSelection))]
[RequireComponent(typeof(AudioSource))]
public class PartyManager : MonoBehaviour {

    [Header("Level structs")]
    public Transform playerTargetCam;
    public GameObject player;
    public Transform startPos;
    public GameObject tryMarker;
    public Transform trySlot;
    public float slotSpacing;
    [HideInInspector]
    public List<GameObject> slots;
    public GameObject finish;
    public int currentSceneIndex;

    [Header("Audio")]
    AudioSource audioSource;
    public AudioClip winSound;
    public AudioClip slideSound;
    public AudioClip impactSound;
    bool play = false;

    [Header("Screens")]
    public Canvas loseScreen;
    public Canvas winScreen;

    [Header("Tweek this")]
    public float dummyValue;

    public float camSpeed;
    Vector3 vel;
    bool count = false;
    float timerStart = 4;
    float timer = 4;

	void Start () {
        audioSource = GetComponent<AudioSource>();
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

        if (Input.GetButtonDown("Reset"))
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
        audioSource.PlayOneShot(winSound,0.75f);

        Camera.main.GetComponent<CameraRotation>().wining = true;       
        Camera.main.transform.DOMove(playerTargetCam.position, 1f).SetEase(Ease.InQuart);      
        Camera.main.transform.LookAt(player.transform.position); 

    }

    void InitGame()
    {
        count = false;
        player.GetComponent<SlideMovement>().animStart = true;
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

        Camera.main.transform.rotation = Camera.main.GetComponent<CameraRotation>().startRot;
    }

    public void CountTry()
    {
        audioSource.PlayOneShot(impactSound,0.5f);
        GameObject current = slots[slots.Count-1];
        player.GetComponent<SlideMovement>().animeFloat = slots.Count -1;
        slots.Remove(current);
        Destroy(current);
    }

    public void SlideSound()
    {
        if (!play)
        {
            audioSource.PlayOneShot(slideSound,1);
            play = true;
        }        
    }

    public void StopSlideSound()
    {
        play = false;
    }

    public void Mute()
    {
        audioSource.volume = 0;
    }
}
