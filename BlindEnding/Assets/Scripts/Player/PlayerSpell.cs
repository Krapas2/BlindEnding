using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSpell : MonoBehaviour
{
    public float power;
    public float range;
    public float cooldown;
    public float cost;
    public abstract void Cast(Vector2 mousePosition);
}
