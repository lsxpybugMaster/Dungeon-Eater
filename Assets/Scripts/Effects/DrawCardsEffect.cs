using System.Collections.Generic;
using UnityEngine;

public class DrawCardsEffect : Effect
{
    [SerializeField] private int drawAmount;

    //效果无目标,直接忽略targets
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        //DrawCardsEffect对应DrawCardsGA
        DrawCardsGA drawCardsGA = new(drawAmount);
        return drawCardsGA;
    }
}
