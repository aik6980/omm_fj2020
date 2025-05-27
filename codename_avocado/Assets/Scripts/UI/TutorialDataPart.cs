using UnityEngine;

[CreateAssetMenu(fileName = "TutorialDataPart", menuName = "Scriptable Objects/TutorialDataPart")]
public class TutorialDataPart : ScriptableObject
{
    [Header("Text")]
    public string title;

    [TextArea(minLines: 2, maxLines: 4)]
    public string description;

    [Header("Video")]
    public GameObject videoSource;
    public bool loopVideo;
}
