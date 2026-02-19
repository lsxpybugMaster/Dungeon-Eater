
using System;

public class OnPlayerDamagedCondition : PerkCondition
{
    public override bool SubConditionIsMet(GameAction gameAction)
    {
        //只有玩家受伤时才触发
        var dealDamageGA = gameAction as DealDamageGA;
        return dealDamageGA.DamageTaker is HeroView;
    }

    public override void SubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.SubscribeReaction<DealDamageGA>(reaction, reactionTiming);
    }

    public override void UnsubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.UnsubscribeReaction<DealDamageGA>(reaction, reactionTiming);
    }
}
