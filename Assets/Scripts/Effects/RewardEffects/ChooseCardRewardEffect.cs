using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardRewardEffect : Effect, IamRewardEffect
{
    [SerializeField] private int choices;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster, EffectContext context)
    {
        ChooseRewardCardGA chooseRewardCardGA = new(choices);
        return chooseRewardCardGA;
    }

    public GameAction GetRewardGameAction(int amount)
    {
        ChooseRewardCardGA chooseRewardCardGA = new(amount);
        return chooseRewardCardGA;
    }
}
