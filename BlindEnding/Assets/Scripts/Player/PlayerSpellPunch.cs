using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellPunch : PlayerSpell
{
    public GameObject punchObject;

    public override void Cast(Vector2 mousePosition)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

            Vector3 punchOffset = direction * range;
            Instantiate(punchObject, transform.position + punchOffset, Quaternion.identity);
        }
    }
}
