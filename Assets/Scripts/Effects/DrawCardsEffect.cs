using UnityEngine;

public class DrawCardsEffect : Effect
{
    [SerializeField] private int drawAmount;

    public override GameAction GetGameAction()
    {
        //DrawCardsEffect��ӦDrawCardsGA
        DrawCardsGA drawCardsGA = new(drawAmount);
        return drawCardsGA;
    }
}
