using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unfold : MonoBehaviour
{

    [System.Serializable]
    public class SquareFace
    {
        public int parent;
        public Vector2 parentSide;    //which edge of the parent it attaches to (x=right, y=fwd)
        public float angle;           //bent ones start at 90; larger faces are made of multiple unit faces at 0
        public float currentAngle;    //start at angle, all 0 when fully unfolded
        public Transform model;
    }
    //faces are oriented horizontally, inner side up if rotation is default

    public UnfoldShapeDefinition[] shapeDefinitions;
    public List<SquareFace> faces =  new List<SquareFace>();

    public float edgeLength = 1.0f;
    public float unfoldAngVel = 90.0f;  //degrees per second
    public bool waitForParents = true;

    public GameObject facePrefab;

    [Range(0,1)]
    public float progress;

    [InspectorButton("SpawnFaces", "UnfoldStep_", "UnSpawnFaces")]
    public bool _;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    bool AreFacesSpawned()
    {
        for (int i = 0; i < faces.Count; i++)
        {
            if (faces[i].model != null)
                return true;
        }

        return false;
    }

    void SpawnFaces()
    {
        for(int i=0; i<faces.Count; i++)
        {
            if (faces[i].model == null)
            {
                faces[i].model = Instantiate(facePrefab, this.transform.position, this.transform.rotation, this.transform).transform;
            }
            faces[i].currentAngle = faces[i].angle;
        }
        UpdateTransforms(faces);
    }

    void UnSpawnFaces()
    {
        for (int i = 0; i < faces.Count; i++)
        {
            if (faces[i].model)
            {
                Destroy(faces[i].model.gameObject);
            }
        }
    }

    void UnfoldStep_()
    {
        UnfoldStep(0.1f);
    }

    void UnfoldStep(float dT)
    {
        for (int i = 0; i < faces.Count; i++)
        {
            SquareFace f = faces[i];
            if (f.currentAngle <= 0) continue;   //already unfolded
            if (!(f.parent < i)) continue;   //has no parent

            if (waitForParents && faces[faces[i].parent].currentAngle > 0) continue;

            f.currentAngle = Mathf.Max(0, f.currentAngle - unfoldAngVel * dT);
        }

        UpdateTransforms(faces);
    }


    void UpdateTransforms(List<SquareFace> squareFaces)
    {
        for (int i = 0; i < squareFaces.Count; i++)
        {
            SquareFace f = squareFaces[i];
            if (!(f.parent < i)) continue;   //has no parent
            if (f.model == null) continue;

            SquareFace p = squareFaces[squareFaces[i].parent];
            Vector3 v = new Vector3(f.parentSide.x, 0, f.parentSide.y) * edgeLength * 0.5f;
            Vector3 foldAxis = new Vector3(-f.parentSide.y, 0, f.parentSide.x);
            Quaternion relRot = Quaternion.AngleAxis(f.currentAngle, foldAxis);
            f.model.position = p.model.TransformPoint(v + relRot * v);
            f.model.rotation = p.model.rotation * relRot;
        }
    }

    public void UseUnfoldShapeDefinition(int i)
    {
        bool areFacesSpawned = AreFacesSpawned();
        if(areFacesSpawned)
            UnSpawnFaces();

        faces.Clear();
        faces = new List<SquareFace>(shapeDefinitions[i].faces);

        if(areFacesSpawned)
            SpawnFaces();
    }

    public int UnfoldShapeDefinitionAmount()
    {
        return shapeDefinitions.Length;
    }

    public List<Vector2> GetUnfoldedNet()
    {
        List<Vector2> coordinates = new List<Vector2>(faces.Count);

        Vector2 WorldCoordinates(int j)
        {
            if (j <= 0) return Vector2.zero;

            return WorldCoordinates(faces[j].parent) + faces[j].parentSide;
        }

        for (int i = 0; i < faces.Count; ++i)
        {
            coordinates.Add(WorldCoordinates(i));
        }

        return coordinates;
    }

    void OnValidate()
    {
        for (int i = 0; i < faces.Count; i++)
        {
            SquareFace f = faces[i];
            if (!(f.parent < i)) continue;   //has no parent

            f.currentAngle = Mathf.Lerp(f.angle, 0.0f, progress);
        }

        UpdateTransforms(faces);
    }
}
