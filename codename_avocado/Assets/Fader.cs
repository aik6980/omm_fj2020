using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Graphic graphic;
    public Color fade_from_colour = Color.black;

    void Start()
    {
        IEnumerator FadeIn(UnityEngine.UI.Graphic img)
        {
            float a = 0.0f;
            while (a < 1.0f)
            {
                a = Mathf.Clamp(a + (Time.unscaledDeltaTime * 1f), 0f, 1f);
                graphic.color = Color.Lerp(fade_from_colour, Color.white, a);
                yield return null;
            }
        }

        StartCoroutine(FadeIn(graphic));
    }
}
