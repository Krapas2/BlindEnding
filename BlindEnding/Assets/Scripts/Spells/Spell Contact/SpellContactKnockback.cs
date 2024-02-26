using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellContactKnockback : SpellContact
{

    public float force;

    public override void Contact(Collider2D other)
    {
        if (other.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRB))
        {
            if (TryGetComponent<Rigidbody2D>(out Rigidbody2D thisRB))
            {
                otherRB.AddForce(thisRB.velocity * force);
            }
            else
            {
                otherRB.AddForce((Vector2)(transform.position - other.transform.position).normalized * force);
            }
        }
    }
}
