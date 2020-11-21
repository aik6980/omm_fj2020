using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unfold : MonoBehaviour
{

    [System.Serializable]
    public class SquareFace
    {
        public int index;
        public int parent;
        public Vector2 parentSide;    //which edge of the parent it attaches to (x=right, y=fwd)
        public float angle;           //bent ones start at 90; larger faces are made of multiple unit faces at 0
        public float currentAngle;    //start at angle, all 0 when fully unfolded
        public Transform model;
    }
    //faces are oriented horizontally, inner side up if rotation is default

    public List<SquareFace> faces =  new List<SquareFace>();

    public float edgeLength = 1.0f;
    public float unfoldAngVel = 90.0f;  //degrees per second
    public bool waitForParents = true;

    public GameObject facePrefab;

    [InspectorButton("SpawnFaces", "UnfoldStep_", "UnSpawnFaces")]
    public bool _;

    void Start()
    {
        
    }

    void Update()
    {
        
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
        UpdateTransforms();
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

        UpdateTransforms();
    }


    void UpdateTransforms()
    {
        for (int i = 0; i < faces.Count; i++)
        {
            SquareFace f = faces[i];
            if (!(f.parent < i)) continue;   //has no parent

            SquareFace p = faces[faces[i].parent];
            Vector3 v = new Vector3(f.parentSide.x, 0, f.parentSide.y) * edgeLength * 0.5f;
            Vector3 foldAxis = new Vector3(-f.parentSide.y, 0, f.parentSide.x);
            Quaternion relRot = Quaternion.AngleAxis(f.currentAngle, foldAxis);
            f.model.position = p.model.TransformPoint(v + relRot * v);
            f.model.rotation = p.model.rotation * relRot;
        }
    }
}
