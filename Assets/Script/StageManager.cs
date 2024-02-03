using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stage;
    [SerializeField] Sprite[] stageBackgrounds;
    [SerializeField] SpriteRenderer backroundRenderer;
    // Start is called before the first frame update
    void Start()
    {
        stage = PlayerPrefs.GetInt("Stage", 1);

        switch (stage)
        {
            case(1):
                backroundRenderer.sprite = stageBackgrounds[0];
                break;
            case(2):
                backroundRenderer.sprite = stageBackgrounds[1];
                break;
            case(3):
                backroundRenderer.sprite = stageBackgrounds[2];
                break;
            case(4):
                backroundRenderer.sprite = stageBackgrounds[3];
                break;
            case(5):
                backroundRenderer.sprite = stageBackgrounds[4];
                break;

            
        }
    }
}
