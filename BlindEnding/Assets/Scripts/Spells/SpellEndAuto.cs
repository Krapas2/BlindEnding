using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEndAuto : SpellEnd
{
    void Start()
    {
        Invoke("Die", time);
    }
}
