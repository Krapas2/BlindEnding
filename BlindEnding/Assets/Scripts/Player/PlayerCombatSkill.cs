using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCombatSkill : MonoBehaviour
{
    public float Power;
    public float Cooldown;
    public float Cost;
    public abstract void Cast(Vector2 mousePosition);
}
