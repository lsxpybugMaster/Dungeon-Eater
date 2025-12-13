using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    立即结算燃烧状态的效果,可用于燃烧相关卡牌
    以及状态结算
 */
public class ApplyBurnEffect : Effect
{
    [SerializeField] public int burnDamage;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster, EffectContext context)
    {
        ApplyBurnGA ga = new(burnDamage, targets[0]);
        return ga;
    }
}
