using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackGA : DealAttackGA
{
    public NormalAttackGA(int amount, List<CombatantView> targets, CombatantView caster, string diceStr = null, EffectContext ctx = null) : base(diceStr, targets, caster, ctx)
    {
        Damage = amount; //Damage来自父类,由子类实现
    }
}
