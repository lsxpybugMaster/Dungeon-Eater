using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 触发天赋天赋的控制系统
/// 通过将天赋订阅对应的GameAction实现
/// </summary>
public abstract class PerkCondition
{
    [SerializeField] protected ReactionTiming reactionTiming;

    // Action<T> 表示一个无返回值的方法，接受一个T类型参数
    public abstract void SubscribeCondition(Action<GameAction> reaction);

    public abstract void UnsubscribeCondition(Action<GameAction> reaction);

    //额外的判断条件，用于更精细地判断条件的触发
    public abstract bool SubConditionIsMet(GameAction gameAction);
}
