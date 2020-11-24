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
        public int generation;        //used if waitForParents; number of anchestors
        public float currentAngle;    //start at angle, all 0 when fully unfolded
        public Transform model;
    }
    //faces are oriented horizontally, inner side up if rotation is default

    public UnfoldShapeDefinition[] shapeDefinitions;

    public int currentShapeDefIndex = 0;
    public List<SquareFace> faces =  new List<SquareFace>();

    public float edgeLength = 1.0f;
    public float unfoldAngVel = 90.0f;  //degrees per second
    public bool waitForParents = true;
    public int maxGenerations = 0;

    public GameObject facePrefab;

    [Range(0,1)]
    public float progress;

    [InspectorButton("UseShapeDef")]
    public bool __;
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

    public void SpawnFaces()
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

    public void UnSpawnFaces()
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

    public void UnfoldStep(float dT)
    {
        /*
        for (int i = 0; i < faces.Count; i++)
        {
            SquareFace f = faces[i];
            if (f.currentAngle <= 0) continue;   //already unfolded
            if (!(f.parent < i)) continue;   //has no parent

            if (waitForParents && faces[faces[i].parent].currentAngle > 0) continue;

            f.currentAngle = Mathf.Max(0, f.currentAngle - unfoldAngVel * dT);
        }
        
        UpdateTransforms(faces);
        */
        progress = Mathf.Clamp01(progress + dT);
        OnValidate();
    }

    public bool Finished()
    {
        for (int i = 0; i < faces.Count; i++)
        {
            SquareFace f = faces[i];
            if (f.currentAngle <= 0) continue;   //already unfolded
            if (!(f.parent < i)) continue;   //has no parent

            return false;
        }

        return true;
    }

    public int NumFlats()
    {
        int numFlats = 0;
        for (int i = 0; i < faces.Count; i++)
        {
            SquareFace f = faces[i];
            if (f.currentAngle <= 0) numFlats++;
            if (!(f.parent < i)) continue;   //has no parent
        }

        return numFlats;
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

    public void UseShapeDef()
    {
        UseUnfoldShapeDefinition(currentShapeDefIndex);
    }

    public void UseUnfoldShapeDefinition(int i)
    {
        bool areFacesSpawned = AreFacesSpawned();
        if(areFacesSpawned)
            UnSpawnFaces();

        faces.Clear();
        faces = new List<SquareFace>(shapeDefinitions[i].faces);

        currentShapeDefIndex = i;

        if (areFacesSpawned)
            SpawnFaces();
    }

    public int UnfoldShapeDefinitionAmount()
    {
        return shapeDefinitions.Length;
    }

    public List<Vector2Int> GetUnfoldedNet()
    {
        List<Vector2Int> coordinates = new List<Vector2Int>(faces.Count);

        Vector2Int WorldCoordinates(int j)
        {
            if (j <= 0) return Vector2Int.zero;

            return WorldCoordinates(faces[j].parent) + Vector2Int.RoundToInt(faces[j].parentSide);
        }

        for (int i = 0; i < faces.Count; ++i)
        {
            coordinates.Add(WorldCoordinates(i));
        }

        return coordinates;
    }

    float S_Curve(float x)
    {
        //return 3.0f * x * x - 2.0f * x * x * x;
        //return 0.5f - 0.5f * Mathf.Cos(x * Mathf.PI);
        return (x * x) / (2.0f * (x * x - x) + 1.0f);
    }

    void OnValidate()
    {
        //used for the progress slider

        maxGenerations = 0;
        for (int i = 0; i < faces.Count; i++)
        {
            SquareFace f = faces[i];
            f.generation = (f.parent >= i) ? 0
                           : (f.angle <= 0 ? faces[f.parent].generation : faces[f.parent].generation + 1);
            maxGenerations = Mathf.Max(maxGenerations, f.generation);
        }

        for (int i = 0; i < faces.Count; i++)
        {
            SquareFace f = faces[i];
            if (!(f.parent < i))   //has no parent
            {
                f.currentAngle = 0;
                continue;
            }

            if (waitForParents)
            {
                float genLen = 1.0f / (float)(maxGenerations);
                float genStart = (f.generation - 1) * genLen;
                float genProgress = Mathf.Clamp01((progress - genStart) / genLen);  //progress of this generation
                //ToDo: add a bit of pause at each stage :) it would feel better
                f.currentAngle = Mathf.Lerp(f.angle, 0.0f, S_Curve(genProgress));
            }
            else
            {
                f.currentAngle = Mathf.Lerp(f.angle, 0.0f, progress);
            }
        }

        UpdateTransforms(faces);
    }
}
