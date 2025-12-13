using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalHitGA : DealAttackGA
{
    public CriticalHitGA(int amount, List<CombatantView> targets, CombatantView caster, string diceStr = null, EffectContext ctx = null) : base(diceStr, targets, caster, ctx)
    {
        Damage = amount;
    }
}
