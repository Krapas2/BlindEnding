using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellContactManager : MonoBehaviour
{
    public LayerMask ignore;
    private SpellContact[] effects;
    private bool instant;

    private HashSet<Collider2D> contacted;

    void Start()
    {
        contacted = new HashSet<Collider2D>();
        effects = GetComponents<SpellContact>();

        instant = true;
        foreach (SpellContact effect in effects)
        {
            instant &= !effect.continuous;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (IgnoreObject(other.gameObject) && instant && TryGetComponent<SpellEndTriggered>(out SpellEndTriggered end))
        {
            end.Trigger();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Contact(other);
    }

    void Contact(Collider2D other)
    {
        if (IgnoreObject(other.gameObject))
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

    bool IgnoreObject(GameObject other)
    {
        return !(ignore == (ignore | (1 << other.layer)));
    }
}
