using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDisplay : MonoBehaviour
{
    Image A;
    Image B;
    Image C;
    Image D;
    [SerializeField] Sprite Up;
    [SerializeField] Sprite Down;
    [SerializeField] Sprite Left;
    [SerializeField] Sprite Right;
    // Start is called before the first frame update

    private static Sprite[] sprites = new Sprite[4];
    private Image[] display = new Image[4];

    void Awake()
    {
        A = this.gameObject.transform.GetChild(3).gameObject.GetComponent<Image>();
        B = this.gameObject.transform.GetChild(2).gameObject.GetComponent<Image>();
        C = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();
        D = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();

        display[0] = A;
        display[1] = B;
        display[2] = C;
        display[3] = D;

        sprites[0] = Up;
        sprites[1] = Left;
        sprites[2] = Down;
        sprites[3] = Right;

    }

    public void SetUp()
    {

        for(int i = 0; i < 4; ++i)
        {
            display[i].gameObject.SetActive(true);
            display[i].sprite = sprites[GameLogic.inputs[i]-1];
            //display[i].transform.Rotate(0.0f, 0.0f, rot[GameLogic.inputs[i]-1], Space.World);
            //display[i].transform.localRotation = Quaternion.Euler(0, 0, rot[GameLogic.inputs[i]-1]);
        }

    }


    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 4; ++i)
        {
            if(GameLogic.inputs[i] == 0)
            {
                display[i].gameObject.SetActive(false);
            }
        }

    }
}
