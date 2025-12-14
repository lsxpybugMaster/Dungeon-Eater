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
 * 
 * Buff 结算条件:
 *      自动结算(标志型效果) 如护盾,临时精通值
 *      激活时结算(主动)  如燃烧
 *      触发时结算(被动)  如反击
 */

[System.Serializable]
public class StatusEffect : IHaveKey<StatusEffectType>
{
    public StatusEffectType Type;
    public StatusEffectType GetKey() => Type;

    public Sprite sprite;

    //支持外部数据配置
    [field: SerializeReference, SR] public Effect effect;

    /// <summary>
    /// 该状态在回合开始时造成的影响(燃烧,眩晕)
    /// </summary>
    /// <param name="c"></param>
    public virtual void OnTurnStart(Combatant c)
    {  
    }

    /// <summary>
    /// 该状态在回合结束时造成的影响(回复生命,结算状态)
    /// </summary>
    /// <param name="c"></param>
    public virtual void OnTurnEnd(Combatant c) 
    {
        if (effect != null)
        {
            GameAction ga = effect.GetGameAction(new() { c.__view__ }, c.__view__ , null);
            // 该函数会在其他Performer中执行,所以需要加Reaction而非Performer
            ActionSystem.Instance.AddReaction(ga);
        }
    }
}
