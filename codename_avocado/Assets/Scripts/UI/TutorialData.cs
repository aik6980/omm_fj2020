using UnityEngine;

[CreateAssetMenu(fileName = "TutorialData", menuName = "Scriptable Objects/Tutorial Data")]
public class TutorialData : ScriptableObject
{
    public string title;
    public TutorialDataPart[] parts;
}
