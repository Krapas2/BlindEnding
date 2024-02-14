using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerSpellPunch : PlayerSpell
{
    public float recoil;
    public GameObject punchObject;

    private Rigidbody2D rb;
    private bool flip = false;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Cast(Vector2 position)
    {
        if (Input.GetButtonDown("Fire1") && castable && caster.CanAffordMana(cost))
        {
            Vector2 direction = (position - (Vector2)transform.position).normalized;

            CreatePunch(direction);
            Recoil(direction);
            caster.SpendMana(cost);
            StartCoroutine(Cooldown());
        }
    }

    void CreatePunch(Vector3 direction)
    {
        Vector3 punchOffset = direction * range;

        GameObject punch = Instantiate(punchObject, transform.position + punchOffset, Quaternion.LookRotation(flip ? Vector3.forward : Vector3.back, direction));
        punch.GetComponent<Rigidbody2D>().velocity += rb.velocity;

        flip = !flip;
    }
    void Recoil(Vector2 direction)
    {
        rb.velocity += direction * recoil;
    }
}
