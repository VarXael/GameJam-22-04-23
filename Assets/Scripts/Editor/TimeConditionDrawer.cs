using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System;

// gave up

/*[CustomPropertyDrawer(typeof(TimeCondition))]
public class TimeConditionDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        float lineHeight = 18f; // ???

        Rect subPosition = position;
        subPosition.height = lineHeight;
        Action nextLine = () => subPosition.y += 18f;

        if (EditorGUILayout.Foldout(true, label))
        {
            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(subPosition, property.FindPropertyRelative(nameof(TimeCondition.chain)));
            nextLine();

            if (property.FindPropertyRelative(nameof(TimeCondition.children)).arraySize > 0)
            {
                // draw just the children
                DrawChildren(subPosition, property.FindPropertyRelative(nameof(TimeCondition.children)));
            }
            else
            {
                EditorGUI.PropertyField(subPosition, property.FindPropertyRelative(nameof(TimeCondition.time)));
                nextLine();
                EditorGUI.PropertyField(subPosition, property.FindPropertyRelative(nameof(TimeCondition.comparison)));
                nextLine();
                EditorGUI.PropertyField(subPosition, property.FindPropertyRelative(nameof(TimeCondition.children)));
                nextLine();
                DrawChildren(subPosition, property.FindPropertyRelative(nameof(TimeCondition.children)));
            }
            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    private void DrawChildren(Rect position, SerializedProperty childrenProperty)
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(childrenProperty);/*
        for (int index = 0; index < childrenProperty.arraySize; index++)
        {
            OnGUI(position, childrenProperty.GetArrayElementAtIndex(index), new GUIContent(""));
        }*/
     /*   EditorGUILayout.EndVertical();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 100f;*/
//}