using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BodyShape
{
    Cube,
    L,
    Long,
    T,
    Z
}

[CreateAssetMenu(fileName = "ShapeDefinition", menuName = "ScriptableObjects/UnfoldShapeDefinition", order = 1)]
public class UnfoldShapeDefinition : ScriptableObject
{
    public BodyShape bodyShape;

    public Texture thumbnailTexture;
    public Color color = Color.white;

    public List<Unfold.SquareFace> faces;
}
