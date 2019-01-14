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
        GUILayout.Label("Effected By: " + UpdateEffectiveLabel(types), listLabel);
        selectedResisted = EditorGUILayout.Popup("Resistant To:", selectedResisted, types);
        GUILayout.Label("Resists: " + UpdateResistsLabel(types), listLabel);
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
        //UpdateTypes(data);
        UpdateOptions();
        ClearFields();
    }

    private void ClearFields()
    {
        typeName = "";
        selectedEffective = 0;
        selectedResisted = 0;
        effective.Clear();
        resistant.Clear();
    }

    private void UpdateTypes(TypeData data)
    {
        TypeChartEditor.typeFields.Add(data);
        //TypeChartEditor.selectedIndexes.Add(0);
    }

    private void UpdateOptions()
    {
        types = new string[TypeChartEditor.typeFields.Count + 1];

        types[0] = "None";

        for (int i = 0; i < TypeChartEditor.typeFields.Count; i++)
        {
            types[i + 1] = TypeChartEditor.typeFields[i].type_name;
        }

    }

    private string UpdateEffectiveLabel(string[] types)
    {
        string effectiveString = "";
        bool exists = false;

        if (effective.Count == 0 && selectedEffective != 0)
        {
            effective.Add(types[selectedEffective]);
        }
        else if (selectedEffective != 0)
        {
            for (int c = 0; c < effective.Count; c++)
            {
                if (effective[c] == types[selectedEffective])
                {
                    exists = true;
                }
            }

            if (!exists)
                effective.Add(types[selectedEffective]);
        }

        if (effective.Count > 0)
        {
            for (int i = 0; i < effective.Count; i++)
            {
                effectiveString += (effective[i] + ", ");
            }
        }

        return effectiveString;
    }

    private string UpdateResistsLabel(string[] types)
    {
        string resistsString = "";
        bool exists = false;

        if (resistant.Count == 0 && selectedResisted != 0)
        {
            resistant.Add(types[selectedResisted]);
        }
        else if (selectedResisted != 0)
        {
            for (int c = 0; c < resistant.Count; c++)
            {
                if (resistant[c] == types[selectedResisted])
                {
                    exists = true;
                }
            }

            if (!exists)
                resistant.Add(types[selectedResisted]);
        }

        if (resistant.Count > 0)
        {
            for (int i = 0; i < resistant.Count; i++)
            {
                resistsString += (resistant[i] + ", ");
            }
        }
        

        return resistsString;
    }

}
