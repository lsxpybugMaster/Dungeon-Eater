using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : Effect
{
    [SerializeField] private int damageAmount;
    /// <summary>
    /// 从外部传入效果对应的目标
    /// </summary>
    /// <param name="targets"></param>
    /// <returns></returns>
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        //创建GA并返回
        DealFixedAttackGA dealDamageGA = new(damageAmount, targets, caster);

        return dealDamageGA;
    }
}
