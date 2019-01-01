using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TypeCreator : EditorWindow
{
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TypeCreator), true, "Type Creator");
    }
}
