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

    void Awake()
    {
        display[0] = A;
        display[1] = B;
        display[2] = C;
        display[3] = D;
    }

    public void SetUp()
    {

        for(int i = 0; i < 4; ++i)
        {
            display[i].SetActive(true);
            //display[i].transform.Rotate(0.0f, 0.0f, rot[GameLogic.inputs[i]-1], Space.World);
            display[i].transform.localRotation = Quaternion.Euler(0, 0, rot[GameLogic.inputs[i]-1]);
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
