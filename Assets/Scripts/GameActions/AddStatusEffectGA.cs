using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * GameAction使用方法: 1. 先声明GameAction类,定义属性及构造函数
 *                    2. 在对应的System中进行实现: 见StatusEffectSystem
 */
public class AddStatusEffectGA : GameAction
{
    public StatusEffectType StatusEffectType { get; private set; }
    public int StackCount { get; private set; }
    public List<CombatantView> Targets { get; private set; }

    public AddStatusEffectGA(StatusEffectType statusEffectType, int stackCount, List<CombatantView> targets)
    {
        StatusEffectType = statusEffectType;
        StackCount = stackCount;
        Targets = targets;
    }
}
