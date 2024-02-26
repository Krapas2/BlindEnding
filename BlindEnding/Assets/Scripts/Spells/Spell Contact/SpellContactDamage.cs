using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellContactDamage : SpellContact
{
    public float damage = 10;
    public override void Contact(Collider2D other)
    {
        if (other.TryGetComponent<EntityHealth>(out EntityHealth entityHealth))
        {
            entityHealth.ReceiveDamage(damage);
        }
    }
}
