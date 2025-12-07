using SerializeReferenceEditor;
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

/*
 * 通用的状态,包含一些共通的更新逻辑
 * 每一个类映射对应的函数
 */

[System.Serializable]
public class StatusEffect
{
    public StatusEffectType Type;
    public Sprite sprite;
    public int decayPerRound; //每回合的衰减

    //支持外部数据配置
    [field: SerializeReference, SR] public Effect effectOnStart;
    [field: SerializeReference, SR] public Effect effectOnEnd;

    /// <summary>
    /// 该状态在回合开始时造成的影响(燃烧,眩晕)
    /// </summary>
    /// <param name="c"></param>
    public virtual void DoOnTurnStart(Combatant c)
    {
        if (effectOnStart == null)
        {
            return;
        }

        GameAction ga = effectOnStart.GetGameAction(new() {c.view} , c.view);
        // 直接Perform
        ActionSystem.Instance.Perform(ga);
    }

    /// <summary>
    /// 该状态在回合结束时造成的影响(回复生命,结算状态)
    /// </summary>
    /// <param name="c"></param>
    public virtual void UpdateOnTurnEnd(Combatant c) 
    {
        c.RemoveStatusEffect(Type, decayPerRound);
    }
}
