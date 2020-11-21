using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;

/// <summary>
/// This attribute can only be applied to fields because its
/// associated PropertyDrawer only operates on fields (either
/// public or tagged with the [SerializeField] attribute) in
/// the target MonoBehaviour.
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Field)]
public class InspectorButtonAttribute : PropertyAttribute
{
    public static float kDefaultButtonWidth = 80;

    public readonly string MethodName;
    public readonly string MethodName2;
    public readonly string MethodName3;

    private float _buttonWidth = kDefaultButtonWidth;
    public float ButtonWidth
    {
        get { return _buttonWidth; }
        set { _buttonWidth = value; }
    }

    public InspectorButtonAttribute(string MethodName)
    {
        this.MethodName = MethodName;
    }
    public InspectorButtonAttribute(string MethodName, string name2)
    {
        this.MethodName = MethodName;
        this.MethodName2 = name2;
    }
    public InspectorButtonAttribute(string MethodName, string name2, string name3)
    {
        this.MethodName = MethodName;
        this.MethodName2 = name2;
        this.MethodName3 = name3;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
public class InspectorButtonPropertyDrawer : PropertyDrawer
{
    private MethodInfo _eventMethodInfo = null;

    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        InspectorButtonAttribute inspectorButtonAttribute = (InspectorButtonAttribute)attribute;

        int num = 1 + (!string.IsNullOrEmpty(inspectorButtonAttribute.MethodName2) ? 1 : 0) + (!string.IsNullOrEmpty(inspectorButtonAttribute.MethodName3) ? 1 : 0);
        //Rect buttonRect = new Rect(position.x + (position.width - inspectorButtonAttribute.ButtonWidth) * 0.5f, position.y, inspectorButtonAttribute.ButtonWidth, position.height);
        float buttonWidth = position.width / num;

        {
            Rect buttonRect = new Rect(position.x + buttonWidth * 0, position.y, buttonWidth, position.height);
            if (GUI.Button(buttonRect, inspectorButtonAttribute.MethodName))
            {
                Execute(prop, inspectorButtonAttribute.MethodName);
            }
        }

        if (!string.IsNullOrEmpty(inspectorButtonAttribute.MethodName2))
        {
            Rect buttonRect = new Rect(position.x + buttonWidth * 1, position.y, buttonWidth, position.height);
            if (GUI.Button(buttonRect, inspectorButtonAttribute.MethodName2))
            {
                Execute(prop, inspectorButtonAttribute.MethodName2);
            }
        }

        if (!string.IsNullOrEmpty(inspectorButtonAttribute.MethodName3))
        {
            Rect buttonRect = new Rect(position.x + buttonWidth * 2, position.y, buttonWidth, position.height);
            if (GUI.Button(buttonRect, inspectorButtonAttribute.MethodName3))
            {
                Execute(prop, inspectorButtonAttribute.MethodName3);
            }
        }
    }

    void Execute(SerializedProperty prop, string eventName)
    {
        System.Type eventOwnerType = prop.serializedObject.targetObject.GetType();
        //string eventName = inspectorButtonAttribute.MethodName;

        //if (_eventMethodInfo == null)     //cached? (not good for multi-button)
            _eventMethodInfo = eventOwnerType.GetMethod(eventName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        if (_eventMethodInfo != null)
            _eventMethodInfo.Invoke(prop.serializedObject.targetObject, null);
        else
            Debug.LogWarning(string.Format("InspectorButton: Unable to find method {0} in {1}", eventName, eventOwnerType));
    }
}
#endif