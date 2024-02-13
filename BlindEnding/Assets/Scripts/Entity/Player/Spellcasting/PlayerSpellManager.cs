using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerSpellManager : MonoBehaviour
{
    public enum Spell
    {
        areaOfEffect,
        projectile,
        punch,
        stream,
        summon,
        wall,
    }

    [System.Serializable]
    public struct SpellTableItem
    {
        public string name;
        public PlayerSpell spellBehaviour;
        public Spell[] spellCombination;

        public SpellTableItem(string name, Spell[] spellCombination, PlayerSpell spellBehaviour)
        {
            this.name = name;
            this.spellCombination = spellCombination;
            this.spellBehaviour = spellBehaviour;
        }
    }

    public float maxMana = 100;
    [HideInInspector]
    public float currentMana;
    public float manaRegenAcceleration = 30;
    private float manaRegenSpeed;
    public SpellTableItem[] spellTableItems;
    public int spellCombinationAmount = 2;
    public Spell[] availableSpells = new Spell[3];

    private Dictionary<HashSet<Spell>, PlayerSpell> spellTable = new Dictionary<HashSet<Spell>, PlayerSpell>(HashSet<Spell>.CreateSetComparer());
    private List<Spell> selectedSpell = new List<Spell>();

    private CameraData mainCamData;

    void Start()
    {
        mainCamData = Camera.main.gameObject.GetComponent<CameraData>();
        currentMana = maxMana;
        manaRegenSpeed = 0;

        AssignSpellTable();
        AssignStartingSelectedSpell();
    }

    void Update()
    {
        Vector2 mouseDirection = (mainCamData.mouseWorldPosition - (Vector2)transform.position).normalized;

        RegenMana();
        SelectSpell();

        if (Input.GetButton("Fire1"))
        {
            SelectedSpellBehaviour().Cast(mainCamData.mouseWorldPosition);
        }
    }

    void AssignSpellTable()
    {
        foreach (SpellTableItem spellTableItem in spellTableItems)
        {
            spellTable.Add(new HashSet<Spell>(spellTableItem.spellCombination), spellTableItem.spellBehaviour);
        }
    }

    void AssignStartingSelectedSpell()
    {
        for (int i = 0; i < spellCombinationAmount; i++)
        {
            selectedSpell.Add(availableSpells[0]);
        }
    }

    void RegenMana()
    {

        float regenAmount = manaRegenSpeed * Time.deltaTime;
        currentMana = currentMana + regenAmount > maxMana ? maxMana : currentMana + regenAmount;
        manaRegenSpeed += manaRegenAcceleration * Time.deltaTime;
    }

    void SelectSpell()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSelectedSpell(availableSpells[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSelectedSpell(availableSpells[1]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSelectedSpell(availableSpells[2]);
        }
    }

    PlayerSpell SelectedSpellBehaviour()
    {
        HashSet<Spell> spellCombination = new HashSet<Spell>(selectedSpell.ToArray());
        return spellTable[spellCombination];
    }


    void ChangeSelectedSpell(Spell newSpell)
    {
        selectedSpell.RemoveAt(0);
        selectedSpell.Add(newSpell);
    }

    public bool CanAffordMana(float cost)
    {
        return cost <= currentMana;
    }

    public void SpendMana(float cost) //todo: throws error if cost is higher than currentMana;
    {
        manaRegenSpeed = -cost;
        currentMana -= cost;
    }
}
