using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMenu : MonoBehaviour
{
    public UnityEngine.UI.RawImage end_slate;
    public UnityEngine.UI.Image curtain;
    public UnityEngine.UI.Button button;


    private void OnEnable()
    {
        Time.timeScale = 0;
        IEnumerator FadeIn(UnityEngine.UI.Graphic img)
        {
            float a = 0.0f;
            while (a < 1.0f)
            {
                a = Mathf.Clamp(a + (Time.unscaledDeltaTime * 1f), 0f, 1f);
                end_slate.color = Color.Lerp(Color.clear, Color.white, a);
                yield return null;
            }

            button.gameObject.SetActive(true);
            button.interactable = false;
            StartCoroutine(FadeBUTTONIn(end_slate));
        }

        IEnumerator FadeBUTTONIn(UnityEngine.UI.Graphic img)
        {
            float a = 0.0f;
            while (a < 1.0f)
            {
                a = Mathf.Clamp(a + (Time.unscaledDeltaTime * 1f), 0f, 1f);
                button.GetComponent<UnityEngine.UI.Image>().color = Color.Lerp(Color.clear, Color.white, a);
                yield return null;
            }
            button.interactable = true;
        }


        StartCoroutine(FadeIn(end_slate));
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void DO_IT()
    {
        IEnumerator FadeToBlack()
        {
            float a = 0.0f;
            while (a < 1.0f)
            {
                a = Mathf.Clamp(a + (Time.unscaledDeltaTime * 1f), 0f, 1f);
                curtain.color = Color.Lerp(Color.clear, Color.black, a);
                yield return null;
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("BandaidStartMenu");
        }

        StartCoroutine(FadeToBlack());
    }
}
