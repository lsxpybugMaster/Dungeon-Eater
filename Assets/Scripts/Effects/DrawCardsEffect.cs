using UnityEngine;

public class DrawCardsEffect : Effect
{
    [SerializeField] private int drawAmount;

    public override GameAction GetGameAction()
    {
        //DrawCardsEffect∂‘”¶DrawCardsGA
        DrawCardsGA drawCardsGA = new(drawAmount);
        return drawCardsGA;
    }
}
