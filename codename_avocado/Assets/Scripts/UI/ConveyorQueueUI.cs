using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConveyorQueueUI : MonoBehaviour
{
    [Tooltip("When true, will show the selected shape in the UI as being the current one. Otherwise, will show the shape directly afterwards.")]
    private bool showFirstImageAsActive = true;

    public RawImage[] queuedImages;
    public Unfold unfoldScript;
    public GridPlayerCharacter gridPlayerCharacter;

    public void UpdateTextures()
    {
        int[] shapeDefIndices = NextShapeQueue.Instance.PeekAll();

        int i = 0;
        if (showFirstImageAsActive)
        {
            i = 1;
            //If queuedImages is empty this will error
            queuedImages[0].texture = gridPlayerCharacter.m_currentUnfoldShapeDef.thumbnailTexture;
        }

        for (; i < Mathf.Min(shapeDefIndices.Length, queuedImages.Length); i++)
        {
            int shapeIndex = shapeDefIndices[i];
            queuedImages[i].texture =
                unfoldScript.shapeDefinitions[shapeIndex].thumbnailTexture ?? Texture2D.redTexture;
        }
    }
}
