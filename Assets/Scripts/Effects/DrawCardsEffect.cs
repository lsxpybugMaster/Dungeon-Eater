using UnityEngine;

public class DrawCardsEffect : Effect
{
    [SerializeField] private int drawAmount;

    public override GameAction GetGameAction()
    {
        //DrawCardsEffect对应DrawCardsGA
        DrawCardsGA drawCardsGA = new(drawAmount);
        return drawCardsGA;
    }
}
