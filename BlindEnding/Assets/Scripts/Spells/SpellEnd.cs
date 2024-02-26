using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellEnd : MonoBehaviour
{
    public float time;

    public void Trigger()
    {
        Invoke("Die", time);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
