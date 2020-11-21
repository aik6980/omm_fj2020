using UnityEngine;
using System.Collections;

public class EventListener : MonoBehaviour
{
    public delegate void TriggerFunction(Collider col);
    public delegate void CollisionFunction(Collision coll);
    public delegate void GameObjectFunction(GameObject g);
    public delegate void GameObject2Function(GameObject a, GameObject b);

    public event TriggerFunction OnTriggerEnterDelegate = null;
    public event TriggerFunction OnTriggerExitDelegate = null;

    public event CollisionFunction OnCollisionEnterDelegate = null;
    public event CollisionFunction OnCollisionExitDelegate = null;

    public event GameObject2Function OnTriggerEnterDelegate2 = null;
    public event GameObject2Function OnTriggerExitDelegate2 = null;

    public event GameObject2Function OnCollisionEnterDelegate2 = null;
    public event GameObject2Function OnCollisionExitDelegate2 = null;

    public event GameObjectFunction OnDestroyDelegate = null;
    public event GameObjectFunction OnEnableDelegate = null;
    public event GameObjectFunction OnDisableDelegate = null;

    public event GameObjectFunction OnAnimatorIKDelegate = null;

    void OnTriggerEnter(Collider col)
    {
        if (OnTriggerEnterDelegate != null) OnTriggerEnterDelegate(col);
        if (OnTriggerEnterDelegate2 != null) OnTriggerEnterDelegate2(this.gameObject, col.gameObject);
    }

    void OnTriggerExit(Collider col)
    {
        if (OnTriggerExitDelegate != null) OnTriggerExitDelegate(col);
        if (OnTriggerExitDelegate2 != null) OnTriggerExitDelegate2(this.gameObject, col.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (OnCollisionEnterDelegate != null) OnCollisionEnterDelegate(collision);
        if (OnCollisionEnterDelegate2 != null) OnCollisionEnterDelegate2(this.gameObject, collision.gameObject);
    }

    void OnCollisionExit(Collision collision)
    {
        if (OnCollisionExitDelegate != null) OnCollisionExitDelegate(collision);
        if (OnCollisionExitDelegate2 != null) OnCollisionExitDelegate2(this.gameObject, collision.gameObject);
    }

    void OnDestroy()
    {
        if (OnDestroyDelegate != null) OnDestroyDelegate(this.gameObject);
    }

    void OnEnable()
    {
        if (OnEnableDelegate != null) OnEnableDelegate(this.gameObject);
    }

    void OnDisable()
    {
        if (OnDisableDelegate != null) OnDisableDelegate(this.gameObject);
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (OnAnimatorIKDelegate != null) OnAnimatorIKDelegate(this.gameObject);
    }

    public static EventListener Get(Collider c)
    {
        return Get(c.gameObject);
    }

    public static EventListener Get(Component c)
    {
        return Get(c.gameObject);
    }

    public static EventListener Get(GameObject go)
    {
        EventListener el = go.GetComponent<EventListener>();
        return el ?? go.AddComponent<EventListener>();
    }

}
