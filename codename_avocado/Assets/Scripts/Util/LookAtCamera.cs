using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LookAtCamera : MonoBehaviour
{
    public bool useCameraForward = true;
    [Range(0,1)]
    public float verticality = 0.5f;
    public bool backFace = true;
    public bool doUpdateWhileEditing = true;

    void LateUpdate()
    {
        LookAt(Camera.main);
    }

    void LookAt(Camera cam)
    {
        Vector3 fwd = useCameraForward ? -cam.transform.forward : cam.transform.position - this.transform.position;
            fwd.y *= (1.0f-verticality);
        if (backFace)
            fwd = -fwd;
        transform.rotation = Quaternion.LookRotation(fwd);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (doUpdateWhileEditing)
            LookAt(UnityEditor.SceneView.currentDrawingSceneView.camera);
    }
#endif
}
