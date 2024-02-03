using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatBubbleDebat : MonoBehaviour
{
    [SerializeField] private Sprite[] basicSprites;
    [SerializeField] private Sprite attackSprite;
    private int spriteIndex;

    void Start()
    {
        spriteIndex = 0;
    }

    public void NextSprite()
    {
        spriteIndex = (spriteIndex + 1) % 2;
        this.gameObject.GetComponent<Image>().sprite = basicSprites[spriteIndex];
    }

    public void AttackSprite()
    {
        this.gameObject.SetActive(true);
        this.gameObject.GetComponent<Image>().sprite = attackSprite;
    }
    
}
