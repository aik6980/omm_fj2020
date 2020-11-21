using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : MonoBehaviour
{
    public GameObject MarkerPrefab;

    private void Start()
    {
        DatabaseAPI.GetOrCreateInstance().ListenForUnfold(SpawnSphere, Debug.Log);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var pos = this.transform.position;
            pos.x += 1.0f;
            this.transform.position = pos;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var pos = this.transform.position;
            pos.x -= 1.0f;
            this.transform.position = pos;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var pos = this.transform.position;
            pos.z += 1.0f;
            this.transform.position = pos;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var pos = this.transform.position;
            pos.z -= 1.0f;
            this.transform.position = pos;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            var pos = this.transform.position;
            var data = new DatabaseAPI.UnfoldData() { x = Mathf.RoundToInt(pos.x), y = Mathf.RoundToInt(pos.z), creator = "Me!", piece_shape = "L" };
            DatabaseAPI.GetOrCreateInstance().SyncUnfold(data, d => Debug.Log(string.Format("Sending unfold at ({0}, {1})", data.x, data.y)), Debug.LogError);
        }
    }

    private void SpawnSphere(DatabaseAPI.UnfoldData data)
    {
        Debug.Log(string.Format("'{0}' created an '{1}' at ({2}, {3})", data.creator, data.piece_shape, data.x, data.y));
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(data.x, -1f, data.y);
        sphere.transform.localScale *= 0.5f;
    }
}
