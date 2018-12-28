using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Type", menuName = "Type")]
public class TypeData : ScriptableObject
{
    public string type_name;
    public List<string> Effective = new List<string>();
    public List<string> Resists = new List<string>();
}
