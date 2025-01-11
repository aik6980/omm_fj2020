using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(MonoBehaviour), true)]
public class ButtonAttributeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Get the target MonoBehaviour instance
        var monoBehaviour = (MonoBehaviour)target;

        // Get all methods with the ButtonAttribute
        var methods = monoBehaviour.GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => m.GetCustomAttribute<ButtonAttribute>() != null);

        foreach (var method in methods)
        {
            var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
            var buttonLabel = string.IsNullOrEmpty(buttonAttribute.Label) ? method.Name : buttonAttribute.Label;

            if (GUILayout.Button(buttonLabel))
            {
                method.Invoke(monoBehaviour, null);
            }
        }
    }
}
