using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TypeChartEditor : EditorWindow
{
    private string typeField;
    private string effectField;
    private Vector2 scrollPosY;
    private Vector2 scrollPosX;
    private List<string> typeFields = new List<string>();
    private List<string> effectFields = new List<string>();

    [MenuItem("Window/TypeChart")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TypeChartEditor), false, "Type Chart");
    }

    private void OnGUI()
    {
        GUIStyle textStyle = new GUIStyle() { alignment = TextAnchor.MiddleCenter, margin = new RectOffset(2, 2, 2, 2) };
        GUIStyle buttonPositioning = new GUIStyle() { alignment = TextAnchor.MiddleRight };
        GUIStyle positionCenter = new GUIStyle() { alignment = TextAnchor.MiddleCenter };

        PopulateStrings();
        textStyle.normal.background = MakeTex(1, 1, Color.white);
        textStyle.normal.textColor = Color.blue;
        EditorGUILayout.BeginVertical();
        scrollPosY = EditorGUILayout.BeginScrollView(scrollPosY);
        GUILayout.BeginHorizontal(buttonPositioning);
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add New", GUILayout.Width(61)))
        {
            TypeCreator.ShowWindow();
        }
        GUILayout.EndHorizontal();
        for (int i = 0; i < 5; i++)
        {
            EditorGUILayout.BeginHorizontal();

            typeFields[i] = EditorGUILayout.TextField(typeFields[i], textStyle, GUILayout.Height(70), 
                GUILayout.Width(70));
            effectFields[i] = EditorGUILayout.TextField(effectFields[i], textStyle, GUILayout.Height(70), 
                GUILayout.Width(70));

            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        scrollPosX = EditorGUILayout.BeginScrollView(scrollPosX);
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
    }

    private void PopulateStrings()
    {
        for (int i = 0; i < 5; i++)
        {
            string test = "";
            string testing = "";

            typeFields.Add(test);
            effectFields.Add(testing);
        }
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

}
