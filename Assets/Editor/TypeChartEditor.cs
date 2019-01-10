using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TypeChartEditor : EditorWindow
{
    //private string typeField;
    //private string effectField;
    private readonly string emptyLabel = " ";
    private string[] multiplier = new string[] { "Halfx", "1x", "2x"};
    private Vector2 scrollPosY;
    private Vector2 scrollPosX;
    public static List<TypeData> typeFields = new List<TypeData>();
    //public static List<int> selectedIndexes = new List<int>();
    //private List<string> effectFields = new List<string>();

    [MenuItem("Window/TypeChart")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TypeChartEditor), false, "Type Chart");
    }

    private void OnGUI()
    {
        GUIStyle textStyle = new GUIStyle() { alignment = TextAnchor.MiddleCenter, margin = new RectOffset(2, 2, 2, 2) };
        //GUIStyle labelStyle = new GUIStyle() { alignment = TextAnchor.MiddleCenter};
        GUIStyle buttonPositioning = new GUIStyle() { alignment = TextAnchor.MiddleRight };
        GUIStyle positionCenter = new GUIStyle() { alignment = TextAnchor.MiddleCenter };

        EditorGUILayout.BeginVertical();
        scrollPosY = EditorGUILayout.BeginScrollView(scrollPosY);
        GUILayout.BeginHorizontal(buttonPositioning);
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add New", GUILayout.Width(61)))
        {
            TypeCreator.ShowWindow();
        }
        GUILayout.EndHorizontal();
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
                        //textStyle.normal.textColor = Color.blue;
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

                    /*EditorGUILayout.TextField(" ", textStyle, GUILayout.Height(70),
                        GUILayout.Width(70));*/
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
        }
    }

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
    private void RemoveListData()
    {
        for (int i = 0; i < typeFields.Count; i++)
        {
            if (typeFields[i] == null)
                typeFields.Remove(typeFields[i]);
        }
    }

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

    private int CheckMultiplierIndex(string num)
    {
        for (int i = 0; i < multiplier.Length; i++)
        {
            if (num.Equals(multiplier[i]))
                return i;
        }
        return 0;
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
