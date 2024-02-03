using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    public GameObject HelpMenu;

    public void OpenHelp()
    {
        HelpMenu.SetActive(true);
    }
    public void CloseHelp()
    {
        HelpMenu.SetActive(false);
    }
}
