using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDebugMenu : MonoBehaviour
{
    public GameObject DebugMenu;
    public static bool ShowDebugMenu = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (!ShowDebugMenu)
            {
                DebugMenu.SetActive(true);
                ShowDebugMenu = true;
            }
            else
            {
                DebugMenu.SetActive(false);
                ShowDebugMenu = false;
            }
        }
    }
}
