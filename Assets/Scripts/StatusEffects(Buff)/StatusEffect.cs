using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectType
{
    AMROR,
    BURN,
    PROFICIENCY, //临时熟练度
    FLEXBILITY, //临时敏捷度
}

//通用的状态,包含一些共通的更新逻辑
[System.Serializable]
public class StatusEffect
{
    public StatusEffectType Type;
    public Sprite sprite;
    public int decayPerRound; //每回合的衰减
    //public virtual void OnTurnStart(Combatant c) 
    //{

    //}

    /// <summary>
    /// 每一个类映射一个函数
    /// </summary>
    /// <param name="c"></param>
    /// <returns>返回值代表是否需要删除状态</returns>
    public virtual void UpdateOnTurnEnd(Combatant c) 
    {
        c.RemoveStatusEffect(Type, decayPerRound);
    }
}
