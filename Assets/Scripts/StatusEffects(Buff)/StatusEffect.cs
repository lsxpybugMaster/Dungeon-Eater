using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectType
{
    AMROR,
    BURN,
    PROFICIENCY, //临时熟练度
}

//通用的状态,包含一些共通的更新逻辑
[System.Serializable]
public class StatusEffect
{
    public StatusEffectType Type;
    public bool clearPerRound = false; //是否每回合清空
    public int decayPerRound; //每回合的衰减
    public virtual void OnTurnStart(Combatant c) 
    {

    }

    //每一个类映射一个函数
    public virtual void OnTurnEnd(Combatant c) 
    {
        if (clearPerRound)
        {
            c.ClearStatusEffect(Type);
        }
        else
        {
            c.RemoveStatusEffect(Type, decayPerRound);
        }
    }
}
