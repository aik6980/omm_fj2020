using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUiController : MonoBehaviour
{
    public TMPro.TMP_Text title;
    public TMPro.TMP_Text description;
    public Image videoRenderTarget;
    public Button prevButton;
    public Button nextButton;
    public Button skipButton;

    [Header("Show/Hide")]
    public float panelFadeTime = 0.1f;
    public float panelFadeInMultiplier = 1.0f;
    public float panelFadeOutMultiplier = 1.0f;

    [Header("Video")]
    public float videoFadeTime = 0.1f;
    public float videoFadeInMultiplier = 1.0f;
    public float videoFadeOutMultiplier = 1.0f;
    public bool videoLoop = true;

    private int currentPart = 0;
    private TutorialDataPart[] tutorialParts;
    private bool viewedItAll = false;
    private CanvasGroup selfFadeMask;
    private TMPro.TMP_Text skipText;

    public void Awake()
    {
        prevButton.onClick.AddListener(GoToPreviousPart);
        nextButton.onClick.AddListener(GoToNextPart);
        skipButton.onClick.AddListener(SkipTutorial);
    }

    public void SetupUi(TutorialDataPart[] parts)
    {
        prevButton.gameObject.SetActive(parts.Length > 1);
        nextButton.gameObject.SetActive(parts.Length > 1);

        selfFadeMask = GetComponent<CanvasGroup>();
        skipText = skipButton.GetComponentInChildren<TMPro.TMP_Text>();

        tutorialParts = parts;
        viewedItAll = false;
        SetCurrentPart(0);
    }

    public void ShowUi()
    {
        selfFadeMask.alpha = 0;
        selfFadeMask.interactable = false;
        gameObject.SetActive(true);

        StartCoroutine(FadeIn());
    }

    private void GoToPreviousPart() => SetCurrentPart(System.Math.Clamp(--currentPart, 0, tutorialParts.Length - 1));
    private void GoToNextPart() => SetCurrentPart(System.Math.Clamp(++currentPart, 0, tutorialParts.Length - 1));
    private void SkipTutorial() => StartCoroutine(FadeOut());

    private void SetCurrentPart(int partIndex)
    {
        currentPart = partIndex;
        title.SetText(tutorialParts[currentPart].title);
        description.SetText(tutorialParts[currentPart].description);
        videoLoop = tutorialParts[currentPart].loopVideo;

        prevButton.interactable = (currentPart > 0);
        nextButton.interactable = (currentPart < tutorialParts.Length - 1);

        viewedItAll |= (currentPart == tutorialParts.Length - 1);
        if (viewedItAll)
        {
            skipText.text = "Done";
        }
        else
        {
            skipText.text = "Skip";
        }
    }

    private IEnumerator FadeIn()
    {
        float fadeTime = panelFadeTime * panelFadeInMultiplier;
        float fadeStartTime = Time.time;
        while (Time.time < fadeStartTime + fadeTime)
        {
            selfFadeMask.alpha = Mathf.Lerp(0.0f, 1.0f, (Time.time - fadeStartTime) / fadeTime);
            yield return 0;
        }

        selfFadeMask.alpha = 1.0f;
        selfFadeMask.interactable = true;
    }

    private IEnumerator FadeOut()
    {
        selfFadeMask.interactable = false;
        float fadeTime = panelFadeTime * panelFadeOutMultiplier;
        float fadeStartTime = Time.time;
        while (Time.time < fadeStartTime + fadeTime)
        {
            selfFadeMask.alpha = Mathf.Lerp(1.0f, 0.0f, (Time.time - fadeStartTime) / fadeTime);
            yield return 0;
        }

        selfFadeMask.alpha = 0.0f;
        gameObject.SetActive(false);
    }
}
