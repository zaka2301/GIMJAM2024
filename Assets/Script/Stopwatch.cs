using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    public IEnumerator StartStopwatch()
    {
        transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.5f);
            transform.GetChild(0).transform.Rotate(0, 0, -18);
        }
    }
}
