using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Menu : MonoBehaviour {

    public bool tutoButton;
    public Transform target;
    public Canvas menu;
    public Canvas tuto;

	public void MoveCamTo()
    {
        if (tutoButton)
        {
            menu.gameObject.SetActive(false);
            tuto.gameObject.SetActive(true);
        }
        else
        {
            menu.gameObject.SetActive(true);
            tuto.gameObject.SetActive(false);
        }
        Camera.main.transform.DOMove(target.position, 1).SetEase(Ease.InQuart);
    }
}
