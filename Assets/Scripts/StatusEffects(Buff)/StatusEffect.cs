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
    DIZZY,  //眩晕
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
    public int decay; //衰减(每回合/每次执行)
    public int decayPerEffect;

    //支持外部数据配置
    [field: SerializeReference, SR] public Effect effectOnStart;
    [field: SerializeReference, SR] public Effect effectOnEnd;

    /// <summary>
    /// 该状态在回合开始时造成的影响(燃烧,眩晕)
    /// </summary>
    /// <param name="c"></param>
    public virtual void OnTurnStart(Combatant c)
    {
        if (effectOnStart == null)
        {
            return;
        }

        GameAction ga = effectOnStart.GetGameAction(new() {c.__view__} , c.__view__);
        // 该函数会在其他Performer中执行,所以需要加Reaction而非Performer
        ActionSystem.Instance.AddReaction(ga);

        c.RemoveStatusEffect(Type, decayPerEffect);
    }

    /// <summary>
    /// 该状态在回合结束时造成的影响(回复生命,结算状态)
    /// </summary>
    /// <param name="c"></param>
    public virtual void OnTurnEnd(Combatant c) 
    {
        c.RemoveStatusEffect(Type, decay);
    }
}
