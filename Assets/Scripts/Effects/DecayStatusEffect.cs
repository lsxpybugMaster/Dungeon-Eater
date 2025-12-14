using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayStatusEffect : Effect
{
    [SerializeField] private int decay;
    [SerializeField] private StatusEffectType type;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster, EffectContext context)
    {
        DecayEffectGA decayEffectGA = new(decay, type, caster);
        return decayEffectGA;
    }
}
