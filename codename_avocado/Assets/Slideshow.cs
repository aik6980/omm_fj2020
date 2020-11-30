using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slideshow : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public Sprite[] slides;
    public float slide_show_delay = 5f;

    private void OnEnable()
    {
        StartCoroutine(DoShow());
    }

    IEnumerator DoShow()
    {
        //foreach (var slide in slides)
        for (int i = 0; i < slides.Length; ++i)
        {
            //image.color = Color.clear;
            image.sprite = slides[i];
            float a = 0.0f;
            if (i > 0)
            {
                while (a < 1.0f)
                {
                    a = Mathf.Clamp(a + Time.unscaledDeltaTime, 0f, 1f);
                    image.color = Color.Lerp(Color.clear, Color.white, a);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(slide_show_delay);

            if (i < slides.Length - 1)
            {
                a = 0.0f;
                while (a < 1.0f)
                {
                    a = Mathf.Clamp(a + Time.unscaledDeltaTime, 0f, 1f);
                    image.color = Color.Lerp(Color.white, Color.clear, a);
                    yield return null;
                }
            }
        }
    }
}
