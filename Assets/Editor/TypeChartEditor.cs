using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TypeChartEditor : EditorWindow
{
    public static List<TypeData> typeFields = new List<TypeData>();

    private readonly string emptyLabel = " ";
    private string[] multiplier = new string[] { "Halfx", "1x", "2x"};
    private Vector2 scrollPosY;
    private Vector2 scrollPosX;

    [MenuItem("Window/TypeChart")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TypeChartEditor), false, "Type Chart");
    }

    private void OnGUI()
    {
        GUIStyle textStyle = new GUIStyle() { alignment = TextAnchor.MiddleCenter, margin = new RectOffset(2, 2, 2, 2) };
        GUIStyle labelStyle = new GUIStyle() { alignment = TextAnchor.MiddleCenter};
        GUIStyle buttonPositioning = new GUIStyle() { alignment = TextAnchor.MiddleRight };

        GetTypes();
        EditorGUILayout.BeginVertical();

        scrollPosY = EditorGUILayout.BeginScrollView(scrollPosY);
        GUILayout.BeginHorizontal(buttonPositioning);
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add New", GUILayout.Width(61)))
        {
            TypeCreator.ShowWindow();
        }
        GUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField(" ", labelStyle, GUILayout.Height(17), GUILayout.Width(70));
        labelStyle.normal.background = MakeTex(1, 1, Color.yellow);
        EditorGUILayout.LabelField("DEFENDING", labelStyle, GUILayout.Height(17), 
            GUILayout.Width(70 * typeFields.Count));

        EditorGUILayout.EndHorizontal();
        RemoveListData();
        for (int i = 0; i < typeFields.Count + 1; i++)
        {
            EditorGUILayout.BeginHorizontal();
            if (i.Equals(0))
            {
                for (int index = 0; index < typeFields.Count + 1; index++)
                {
                    if (index.Equals(0))
                    {
                        EditorGUILayout.TextField(emptyLabel, textStyle, GUILayout.Height(70),
                            GUILayout.Width(70));
                    }
                    else
                    {
                        textStyle.normal.background = MakeTex(1, 1, Color.white);
                        typeFields[index - 1].type_name = EditorGUILayout.TextField(typeFields[index - 1].type_name, textStyle, GUILayout.Height(70),
                            GUILayout.Width(70));
                    }
                }
            }
            else
            {
                textStyle.normal.background = MakeTex(1, 1, Color.white);
                textStyle.normal.textColor = Color.black;
                typeFields[i - 1].type_name = EditorGUILayout.TextField(typeFields[i - 1].type_name, textStyle, GUILayout.Height(70),
                    GUILayout.Width(70));
                for (int c = 0; c < typeFields.Count; c++)
                {
                    int selectedIndex = 0;

                    ColorCoding(GetSelectedIndex(typeFields[i - 1], typeFields[c]), textStyle);
                    selectedIndex = EditorGUILayout.Popup(GetSelectedIndex(typeFields[i - 1], typeFields[c]), multiplier, textStyle, GUILayout.Height(70),
                        GUILayout.Width(70));
                    UpdateEffectiveness(typeFields[i -1], typeFields[c], selectedIndex);
                }
            }
            EditorGUILayout.EndHorizontal();

        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        scrollPosX = EditorGUILayout.BeginScrollView(scrollPosX);
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();
    }

    //Grabs the TypeData Scriptable Objects and Adds them to a List
    private void GetTypes()
    {
        TypeData[] data = Resources.FindObjectsOfTypeAll<TypeData>();

        typeFields.Clear();
        foreach (TypeData types in data)
        {
            typeFields.Add(types);
        }
    }

    //Updates changes made to the Types on the Type Chart and Updates the TypeData Scriptable Object
    private void UpdateEffectiveness(TypeData attack, TypeData defend, int selected)
    {
        if (GetSelectedIndex(attack, defend) != selected)
        {
            for (int i = 0; i < defend.Effective.Count; i++)
            {
                if (defend.Effective[i].Equals(attack.type_name))
                    defend.Effective.Remove(attack.type_name);
            }
            for (int i = 0; i < defend.Resists.Count; i++)
            {
                if (defend.Resists[i].Equals(attack.type_name))
                    defend.Resists.Remove(attack.type_name);
            }

            switch (selected)
            {
                case 0:
                    defend.Resists.Add(attack.type_name);
                    break;
                case 1:
                    break;
                case 2:
                    defend.Effective.Add(attack.type_name);
                    break;
                default:
                    Debug.Log("This option doesn't exist. Check for errors!");
                    break;
            }

            EditorUtility.SetDirty(defend);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    //Changes Colors of Type Chart backgrounds depending on Effectiveness or Resistance
    private void ColorCoding(int multiplier, GUIStyle style)
    {
        switch (multiplier)
        {
            case 0:
                style.normal.background = MakeTex(1, 1, Color.red);
                style.normal.textColor = Color.white;
                break;
            case 1:
                style.normal.background = MakeTex(1, 1, new Color32(209, 209, 209, 255));
                style.normal.textColor = Color.black;
                break;
            case 2:
                style.normal.background = MakeTex(1, 1, new Color32(107, 142, 35, 255));
                style.normal.textColor = Color.white;
                break;
            default:
                Debug.Log("This option doesn't exist. Check for errors!");
                break;
        }
    }

    //Removes Reference to TypeData Scriptable Object on the List if it no longer exists
    private void RemoveListData()
    {
        for (int i = 0; i < typeFields.Count; i++)
        {
            if (typeFields[i].Equals(null))
                typeFields.Remove(typeFields[i]);
        }
    }

    //Gets the Index for Type Effectiveness and Resistance for use in the Popup GUI
    private int GetSelectedIndex(TypeData attack, TypeData defend)
    {
        for (int i = 0; i < defend.Effective.Count; i++)
        {
            if (attack.type_name.Equals(defend.Effective[i]))
            {
                return CheckMultiplierIndex("2x");
            }
        }
        for (int index = 0; index < defend.Resists.Count; index++)
        {

            if (attack.type_name.Equals(defend.Resists[index]))
            {
                return CheckMultiplierIndex("1/2x");
            }
        }

        return CheckMultiplierIndex("1x");
    }

    //Checks if the string is equal to any of the multiplier options
    private int CheckMultiplierIndex(string num)
    {
        for (int i = 0; i < multiplier.Length; i++)
        {
            if (num.Equals(multiplier[i]))
                return i;
        }
        return 0;
    }

    //Creates a Texture based on Dimensions and Color Choice
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
