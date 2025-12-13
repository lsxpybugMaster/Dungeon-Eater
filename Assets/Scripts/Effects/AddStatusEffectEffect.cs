using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Effect构建方法:
    1. override GetGameAction
    2. 定义需要外部传入的字段,将作为构造函数参数
    3. 在其中调用构造函数实例化对应GameAction
 */
public class AddStatusEffectEffect : Effect
{
    [SerializeField] private StatusEffectType statusEffectType;
    
    [SerializeField] private int stackCount;
    
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster, EffectContext context)
    {
        return new AddStatusEffectGA(statusEffectType, stackCount, targets);
    }

}
