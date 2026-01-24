using System.Collections.Generic;
using UnityEngine;

public class AddCardToPileEffect : Effect
{
    [SerializeField] private List<CardData> cardData;
    [SerializeField] private PileType pileType;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster, EffectContext context)
    {
        AddCardGA addCardGA = new(pileType, caster, cardData);
        return addCardGA;
    }
}
