using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ScreenReference))]
public class ScreenReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var screenObjectProp = property.FindPropertyRelative("ScreenObject");
        var selectedObject = EditorGUI.ObjectField(position, label, screenObjectProp.objectReferenceValue, typeof(MonoBehaviour), true);

        if (selectedObject != null && !(selectedObject is IScreen))
        {
            selectedObject = null;
        }

        screenObjectProp.objectReferenceValue = selectedObject;
        EditorGUI.EndProperty();
    }
}
