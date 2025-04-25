using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public TMPro.TMP_Text counter;
    public Image sprite;
    public CanvasGroup fadeGroup;

    public Color finalColour;
    public Color startColour;
    private Color currentColour;

    public float thresholdTime;
    public float fadeInTime;

    // For debugging only...
    //[Range(0.0f, 60.0f)]
    //public float t;
    //
    //private void Update()
    //{
    //    UpdateTimeRemaining(t);
    //}

    public void UpdateTimeRemaining(float time)
    {
        float tValue = Mathf.Clamp01(time / thresholdTime);
        Vector3 startColourHsv = Vector3.zero;
        Color.RGBToHSV(startColour, out startColourHsv.x, out startColourHsv.y, out startColourHsv.z);

        Vector3 finalColourHsv = Vector3.zero;
        Color.RGBToHSV(finalColour, out finalColourHsv.x, out finalColourHsv.y, out finalColourHsv.z);

        Vector3 intermediateColour = Vector3.LerpUnclamped(finalColourHsv, startColourHsv, tValue);
        currentColour = Color.HSVToRGB(intermediateColour.x, intermediateColour.y, intermediateColour.z);

        sprite.color = currentColour;
        counter.color = currentColour;
        counter.text = time.ToString("0.0");

        transform.localScale = new Vector3(0.15f, 0.15f, 1.0f) * (1.0f - (tValue / 2.0f));
        fadeGroup.alpha = Mathf.Clamp01(1.0f - (time - thresholdTime + fadeInTime));
    }
}
