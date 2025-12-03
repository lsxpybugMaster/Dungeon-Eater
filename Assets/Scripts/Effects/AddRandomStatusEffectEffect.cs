using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 添加随机层数的Effect
/// SRP要求允许小型冗余
/// </summary>
public class AddRandomStatusEffectEffect : Effect
{
    [SerializeField] private StatusEffectType statusEffectType;

    [SerializeField] private string stackCountDice;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        return new AddRandomStatusEffectGA(statusEffectType, stackCountDice, targets);
    }
}
