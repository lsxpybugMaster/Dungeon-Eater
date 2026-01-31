using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddManaEffect : Effect
{
    [SerializeField] private bool refill = false;
    [SerializeField] private int addAmount;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster, EffectContext context)
    {
        AddManaGA addManaGA = new(addAmount, refill);
        return addManaGA;
    }
}
