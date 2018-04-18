using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour {


    public void NextLevel(int currentIndex)
    {
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToXScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
