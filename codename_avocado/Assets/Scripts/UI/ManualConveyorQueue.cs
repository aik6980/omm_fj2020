using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConveyorQueue", menuName = "ScriptableObjects/ManualConveyorQueue", order = 1)]
public class ManualConveyorQueue : ScriptableObject
{
    public List<UnfoldShapeDefinition> shapeQueue;
}
