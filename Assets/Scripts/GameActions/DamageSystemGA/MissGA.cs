using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissGA : DealAttackGA
{
    public MissGA(List<CombatantView> targets, CombatantView caster, string diceStr = null, EffectContext ctx = null) : base(diceStr, targets, caster, ctx)
    {
        
    }
}
