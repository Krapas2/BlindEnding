using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellContactManager : MonoBehaviour
{
    public LayerMask ignore;
    private SpellContact[] effects;

    private HashSet<Collider2D> contacted;
    void Start()
    {
        contacted = new HashSet<Collider2D>();
        effects = GetComponents<SpellContact>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Contact(other);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Contact(other);
    }

    void Contact(Collider2D other)
    {
        if (!(ignore == (ignore | (1 << other.gameObject.layer))))
        {
            foreach (SpellContact effect in effects)
            {
                if (contacted.Contains(other) && !effect.continuous)
                {
                    return;
                }
                effect.Contact(other);
            }
            contacted.Add(other);
        }
    }
}
