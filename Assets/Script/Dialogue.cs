using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string[] names;
    public Sprite[] scenes;

    [TextArea(3, 10)]
    public string[] sentences;
    public AudioClip[] sounds;
}
