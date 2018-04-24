using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SceneSelection))]
[RequireComponent(typeof(AudioSource))]
public class PartyManager : MonoBehaviour {

    [Header("Level structs")]
    public GameObject cam;
    public Transform playerTargetCam;
    public GameObject player;
    public Transform startPos;
    public GameObject tryMarker;
    public Transform trySlot;
    public float slotSpacing;
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

        cam.GetComponent<CameraRotation>().wining = true;       
        cam.transform.DOMove(playerTargetCam.position, 1f).SetEase(Ease.InQuart);      
        Camera.main.transform.LookAt(player.transform.position); 

        /*if (timer <= 0)
        {
            Debug.Log("change scene");
            GetComponent<SceneSelection>().NextLevel(currentSceneIndex);
        }*/
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

        Camera.main.transform.rotation = Camera.main.GetComponent<CameraRotation>().startRot;
    }

    public void CountTry()
    {
        audioSource.PlayOneShot(impactSound,0.75f);
        GameObject current = slots[slots.Count-1];
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
