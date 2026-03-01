using System.Collections.Generic;
using UnityEngine;

public class GetMoneyEffect : Effect, IamRewardEffect
{
    /// <summary>
    /// 这个属性既可以通过编辑器配置, 也可以通过RewardSystem在运行时配置
    /// </summary>
    [SerializeField] private int Amount;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster, EffectContext context)
    {
        GetMoneyGA getMoneyGA = new(Amount);
        return getMoneyGA;
    }

    public GameAction GetRewardGameAction(int amount)
    {
        GetMoneyGA getMoneyGA = new(amount);
        return getMoneyGA;
    }
}

