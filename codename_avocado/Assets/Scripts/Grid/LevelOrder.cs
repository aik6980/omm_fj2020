using UnityEngine;

[CreateAssetMenu(fileName = "LevelOrder", menuName = "Scriptable Objects/LevelOrder")]
public class LevelOrder : ScriptableObject
{
    public LevelDataObject[] Levels;
}
