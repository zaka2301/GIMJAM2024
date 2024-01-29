using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDisplay : MonoBehaviour
{
    [SerializeField] GameObject A;
    [SerializeField] GameObject B;
    [SerializeField] GameObject C;
    [SerializeField] GameObject D;
    // Start is called before the first frame update
    private static float[] rot = new float[4]{0.0f, 90.0f, 180.0f, 270.0f};
    private GameObject[] display = new GameObject[4];

    void Start()
    {
        display[0] = A;
        display[1] = B;
        display[2] = C;
        display[3] = D;
        SetUp();

    }

    private void SetUp()
    {
        GameLogic.currentInput = 3;

        for(int i = 0; i < 4; ++i)
        {
            display[i].SetActive(true);
            display[i].transform.Rotate(0.0f, 0.0f, rot[GameLogic.inputs[i]-1], Space.Self);
        }

    }


    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < 4; ++i)
        {
            if(GameLogic.inputs[i] == 0)
            {
                display[i].SetActive(false);
            }
        }

    }
}
