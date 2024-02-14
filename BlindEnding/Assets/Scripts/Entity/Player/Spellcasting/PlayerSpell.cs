using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSpell : MonoBehaviour
{
    public float power;
    public float range;
    public float cooldown;
    public float cost;

    [HideInInspector]
    public bool castable;
    [HideInInspector]
    public PlayerSpellManager caster;

    private CameraData mainCamData;

    public virtual void Start()
    {
        mainCamData = Camera.main.gameObject.GetComponent<CameraData>();
        caster = GetComponent<PlayerSpellManager>();
        castable = true;
    }

    public virtual void Update()
    {
        Cast(mainCamData.mouseWorldPosition);
    }

    public abstract void Cast(Vector2 position);

    public IEnumerator Cooldown()
    {
        castable = false;
        yield return new WaitForSeconds(cooldown);
        castable = true;

        yield return null;
    }
}
