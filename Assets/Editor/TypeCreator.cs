using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TypeCreator : EditorWindow
{
    public string typeName = "";
    public string[] types = new string[] { "None" };

    private int selectedEffective = 0;
    private int selectedResisted = 0;
    private List<string> effective = new List<string>();
    private List<string> resistant = new List<string>();


    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TypeCreator), true, "Type Creator");
    }

    private void OnGUI()
    {
        GUIStyle label = new GUIStyle() { fontStyle = FontStyle.Bold, fontSize = 20, padding = new RectOffset(2, 2, 2, 20),
            alignment = TextAnchor.MiddleCenter };
        GUIStyle listLabel = new GUIStyle() { fontStyle = FontStyle.BoldAndItalic, fontSize = 10,
            padding = new RectOffset(2, 2, 4, 4) };

        UpdateOptions();
        EditorGUILayout.BeginVertical();
        GUILayout.Label("Type Creator", label);
        typeName = EditorGUILayout.TextField("Type Name:", typeName);
        GUILayout.Space(2);
        selectedEffective = EditorGUILayout.Popup("Effected By:", selectedEffective, types);
        GUILayout.Label("Effected By: " + UpdateLabels(types, effective, selectedEffective), listLabel);
        selectedResisted = EditorGUILayout.Popup("Resistant To:", selectedResisted, types);
        GUILayout.Label("Resists: " + UpdateLabels(types, resistant, selectedResisted), listLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear"))
        {
            ClearFields();
        }
        
        if (GUILayout.Button("Create"))
        {
            if(typeName != string.Empty)
                CreateTypeData();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    //Creates a Scriptable Object based on Input Data
    private void CreateTypeData()
    {
        TypeData data = ScriptableObject.CreateInstance<TypeData>();
        data.type_name = typeName;

        for (int i = 0; i < effective.Count; i++)
        {
            data.Effective.Add(effective[i]);
        }

        for (int c = 0; c < resistant.Count; c++)
        {
            data.Resists.Add(resistant[c]);
        }

        AssetDatabase.CreateAsset(data, "Assets/Resources/Types/" + typeName + ".asset");
        EditorUtility.SetDirty(data);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        UpdateOptions();
        ClearFields();
    }

    //Clears the Input Fields
    private void ClearFields()
    {
        typeName = "";
        selectedEffective = 0;
        selectedResisted = 0;
        effective.Clear();
        resistant.Clear();
    }

    //Updates Effective / Resistant Options
    private void UpdateOptions()
    {
        types = new string[TypeChartEditor.typeFields.Count + 1];

        types[0] = "None";

        for (int i = 0; i < TypeChartEditor.typeFields.Count; i++)
        {
            types[i + 1] = TypeChartEditor.typeFields[i].type_name;
        }

    }

    //Updates the Labels for Effective / Resistance
    private string UpdateLabels(string[] types, List<string> label, int selected)
    {
        string effectivenessLabel = "";
        bool exists = false;

        if (label.Count == 0 && selected != 0)
            label.Add(types[selected]);
        else if (selected != 0)
        {
            for (int i = 0; i < label.Count; i++)
            {
                if (label[i].Equals(types[selected]))
                    exists = true;
            }

            if (!exists)
                label.Add(types[selected]);
        }

        if (label.Count > 0)
        {
            for (int i = 0; i < label.Count; i++)
            {
                effectivenessLabel += (label[i] + ", ");
            }
        }
        return effectivenessLabel;
    }
}
