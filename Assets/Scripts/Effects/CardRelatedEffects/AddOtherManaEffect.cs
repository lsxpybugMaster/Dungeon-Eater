using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <see cref="AddManaEffect"/>
/// </summary>
public class AddOtherManaEffect : Effect
{
    [SerializeField] private ManaID manaID;
    [SerializeField] private int addAmount;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster, EffectContext context)
    {
        AddOtherManaGA addOtherManaGA = new(addAmount, manaID);
        return addOtherManaGA;
    }
}
