using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoverScreen : MonoBehaviour
{
    private Image screen;
    // Start is called before the first frame update
    void Start()
    {
        screen = GetComponent<Image>();

        StartCoroutine(Unfade());
    }

    // Update is called once per frame
    IEnumerator Unfade()
    {
        float alpha = 1.0f;
        Color col = new Color(0.0f, 0.0f, 0.0f, alpha);
        while(alpha > 0.0f)
        {
            alpha -= Time.deltaTime;
            col.a = alpha;
            screen.color = col;
            yield return null;
        }
    }
    public IEnumerator Fade()
    {
        float alpha = 0.0f;
        Color col = new Color(0.0f, 0.0f, 0.0f, alpha);
        while(alpha < 1.0f)
        {
            alpha += Time.deltaTime;
            col.a = alpha;
            screen.color = col;
            yield return null;
        }
    }
}
