using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpellContactManager))]
public abstract class SpellContact : MonoBehaviour
{
    public bool continuous = false;
    public abstract void Contact(Collider2D other);
}
