using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerCombat : MonoBehaviour
{
    public enum Skills
    {
        areaOfEffect,
        projectile,
        punch,
        stream,
        summon,
        wall,
    }
    public Skills[] availableSkills = new Skills[3];
    public Skills[] selectedSkill = new Skills[2];
    public Dictionary<HashSet<Skills>, PlayerCombatSkill> skillTable;

    private CameraData mainCamData;

    void Start()
    {
        mainCamData = Camera.main.gameObject.GetComponent<CameraData>();
        skillTable = new Dictionary<HashSet<Skills>, PlayerCombatSkill>(HashSet<Skills>.CreateSetComparer())
        {
            {
                new HashSet<Skills>(selectedSkill),
                GetComponent<PlayerCombatPunch>()
            },
        };
    }

    void Update()
    {
        Vector2 mouseDirection = (mainCamData.mouseWorldPosition - (Vector2)transform.position).normalized;

        if (Input.GetButtonDown("Fire1"))
        {
            foreach (var entry in skillTable)
            {
                HashSet<Skills> skillsSet = entry.Key;
                PlayerCombatSkill combatSkill = entry.Value;

                string skillsString = string.Join(", ", skillsSet.Select(skill => skill.ToString()));
                string combatSkillString = combatSkill.ToString();

                Debug.Log($"Skills Set: {skillsString}, Combat Skill: {combatSkillString}");
            }
            skillTable[new HashSet<Skills>(selectedSkill)].Cast(mainCamData.mouseWorldPosition);
        }
    }
}
