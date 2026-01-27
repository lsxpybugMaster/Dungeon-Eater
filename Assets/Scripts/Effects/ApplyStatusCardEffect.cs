using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyStatusCardEffect : Effect
{
    [SerializeField] private int damage;

    [SerializeField] private StatusEffectType cardStatusEffectType;

    [SerializeField] private GameObject statusVFX;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster, EffectContext context)
    {
        ApplyCardDiscardedEffectGA ga = new(damage, cardStatusEffectType, statusVFX);
        return ga;
    }
}
