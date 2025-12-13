using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDizzyEffect : Effect
{
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster, EffectContext context)
    {
        ApplyDizzyGA ga = new(targets[0]);
        return ga;
    }
}
