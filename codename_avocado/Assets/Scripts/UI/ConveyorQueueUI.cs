using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConveyorQueueUI : MonoBehaviour
{
    [Tooltip("When true, will show the selected shape in the UI as being the current one. Otherwise, will show the shape directly afterwards.")]
    public bool showFirstImageAsActive = true;

    public RawImage[] queuedImages;
    public Unfold unfoldScript;
    public GridPlayerCharacter gridPlayerCharacter;
    public Animator uiAnimator;

    public void UpdateTextures()
    {
        StartCoroutine(PlayCycleAnimation());
    }

    IEnumerator PlayCycleAnimation()
    {
        uiAnimator.SetTrigger("Respawned");

        yield return null;
        yield return new WaitUntil(() => uiAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));

        int[] shapeDefIndices = NextShapeQueue.Instance.PeekAll();

        if (showFirstImageAsActive)
        {
            //If queuedImages is empty this will error
            queuedImages[0].texture = gridPlayerCharacter.m_currentUnfoldShapeDef.thumbnailTexture;

            for (int i = 1; i < Mathf.Min(shapeDefIndices.Length + 1, queuedImages.Length); i++)
            {
                int shapeIndex = shapeDefIndices[i - 1];
                queuedImages[i].texture =
                    unfoldScript.shapeDefinitions[shapeIndex].thumbnailTexture ?? Texture2D.redTexture;
            }
        }
        else
        {
            for (int i = 0; i < Mathf.Min(shapeDefIndices.Length, queuedImages.Length); i++)
            {
                int shapeIndex = shapeDefIndices[i];
                queuedImages[i].texture =
                    unfoldScript.shapeDefinitions[shapeIndex].thumbnailTexture ?? Texture2D.redTexture;
            }
        }
    }
}
