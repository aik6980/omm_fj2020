using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BodyShape
{
    Cube,
    L,
    Long,
    T
}

[CreateAssetMenu(fileName = "ShapeDefinition", menuName = "ScriptableObjects/UnfoldShapeDefinition", order = 1)]
public class UnfoldShapeDefinition : ScriptableObject
{
    public BodyShape bodyShape;

    public List<Unfold.SquareFace> faces;
}
