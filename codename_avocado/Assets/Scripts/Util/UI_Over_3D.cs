using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Over_3D : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    public bool scaleWithDistance = true;
    public float scaleScale = 25.0f;

    RectTransform myRectTrans;
    Canvas canvas;


    void Start()
    {
        myRectTrans = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }

        //this is the ui element
        RectTransform UI_Element;

        //first you need the RectTransform component of your canvas
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();

        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        Vector3 worldPos = target.TransformPoint(targetOffset);
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(worldPos);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        //now you can set the position of the ui element
        myRectTrans.anchoredPosition = WorldObject_ScreenPosition;

        if (scaleWithDistance)
        {
            float distance = Vector3.Dot(worldPos - Camera.main.transform.position, Camera.main.transform.forward);
            myRectTrans.localScale = Vector3.one * (scaleScale / distance);
        }
    }

}
