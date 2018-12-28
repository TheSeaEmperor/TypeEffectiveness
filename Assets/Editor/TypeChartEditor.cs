using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TypeChartEditor : EditorWindow
{
    [MenuItem("Window/TypeChart")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TypeChartEditor), false, "Type Chart");
    }

    private void OnGUI()
    {
        
    }
}
