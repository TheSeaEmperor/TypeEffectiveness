using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public float health;
    public float base_damage;
    public TypeData type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            health -= DamageCalc.CalcDamage(base_damage, type, other.GetComponent<UnitController>().type);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            health -= DamageCalc.CalcDamage(base_damage, type, other.GetComponent<UnitController>().type);
        }

        if (health < 0)
            health = 0;

        if (health <= 0)
            this.gameObject.SetActive(false);
    }
}
