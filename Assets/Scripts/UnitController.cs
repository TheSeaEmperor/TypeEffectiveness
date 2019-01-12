using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int health;
    public int base_damage;
    public TypeData type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            DamageCalc.CalcDamage(base_damage, type, other.GetComponent<UnitController>().type);
        }
    }
}
