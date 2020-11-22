using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "ShapeDefinition", menuName = "ScriptableObjects/UnfoldShapeDefinition", order = 1)]
public class UnfoldShapeDefinition : ScriptableObject
{
    public List<Unfold.SquareFace> faces;
}
