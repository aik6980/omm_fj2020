using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomExitTriggerHandler : MonoBehaviour
{
    public GameObject blockGO;

    bool can_spawn;
    // Start is called before the first frame update
    void Start()
    {
        can_spawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //other.transform.position = new Vector3(0.0f, 1.0f, -13.0f);
        
        if(can_spawn)
        {
            var rand_point2D = Random.insideUnitCircle;
            Instantiate(blockGO, new Vector3(5.0f * rand_point2D.x, 1.0f, -10.0f + 2.0f * rand_point2D.y), Quaternion.identity);
            can_spawn = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        can_spawn = true;
    }
}
