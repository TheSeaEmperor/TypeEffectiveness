using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TypeChartEditor : EditorWindow
{
    private string typeField;
    private string effectField;

    [MenuItem("Window/TypeChart")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TypeChartEditor), false, "Type Chart");
    }

    private void OnGUI()
    {
        GUIStyle textStyle = new GUIStyle
        {
            //fontSize = 10
            //alignment = TextAnchor.MiddleCenter
            border = new RectOffset(0, 50, 0, -50)
        };

        EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
                typeField = EditorGUILayout.TextField(typeField, GUILayout.Width(50), GUILayout.Height(50), 
                    GUILayout.ExpandHeight(false),GUILayout.ExpandWidth(false));
                effectField = EditorGUILayout.TextField(effectField, GUILayout.Width(50), GUILayout.Height(50),
                    GUILayout.ExpandHeight(false), GUILayout.ExpandWidth(false));
            EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
}
