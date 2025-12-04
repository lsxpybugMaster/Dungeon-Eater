using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectType
{
    AMROR,
    BURN,
    PROFICIENCY, //临时熟练度
}

[System.Serializable]
public class StatusEffect
{
    public StatusEffectType Type;
    public virtual void OnTurnStart(Combatant c, int stacks) { }
    public virtual void OnTurnEnd(Combatant c, int stacks) { }
}
