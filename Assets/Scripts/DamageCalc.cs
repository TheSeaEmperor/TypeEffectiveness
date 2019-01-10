using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageCalc
{
    public static float CalcDamage(float damage, TypeData attack, TypeData defend)
    {
        for (int i = 0; i < defend.Effective.Count; i++)
        {
            if (defend.Effective[i].Equals(attack.type_name))
                return damage * 2;
        }
        for (int i = 0; i < defend.Resists.Count; i++)
        {
            if (defend.Resists[i].Equals(attack.type_name))
                return damage / 2;
        }

        return damage;
    }
}
