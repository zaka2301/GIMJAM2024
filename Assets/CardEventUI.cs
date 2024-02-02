using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEventUI : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
 
    public void StartCardEvent()
    {
        gameManager.GetComponent<CardEvent>().StartCardEvent();
    }
}
