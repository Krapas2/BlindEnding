using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatPunch : PlayerCombatSkill
{

    public GameObject punchObject;


    public override void Cast(Vector2 mousePosition)
    {
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        Vector3 punchOffset = direction * 4f;
        Instantiate(punchObject, transform.position + punchOffset, Quaternion.identity);
    }
}
