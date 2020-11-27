using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Graphic graphic;

    void Start()
    {
        IEnumerator FadeIn(UnityEngine.UI.Graphic img)
        {
            float a = 0.0f;
            while (a < 1.0f)
            {
                a = Mathf.Clamp(a + (Time.unscaledDeltaTime * 1f), 0f, 1f);
                graphic.color = Color.Lerp(Color.black, Color.white, a);
                yield return null;
            }
        }

        StartCoroutine(FadeIn(graphic));
    }
}
