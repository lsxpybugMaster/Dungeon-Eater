using System.Collections.Generic;
using UnityEngine;

public class AddCardToPileEffect : Effect
{
    [SerializeField] private CardData cardData;
    [SerializeField] private PileType pileType;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        AddCardGA addCardGA = new(pileType, caster, cardData);
        return addCardGA;
    }
}
