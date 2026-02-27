using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//在战斗开始时触发, 一般是一些战斗即刻生效的Perk
//如开局获取 2 额外资源
public class OnBattleSetupCondition : PerkCondition
{
    public override bool SubConditionIsMet(GameAction gameAction)
    {
        return true;
    }

    public override void SubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.SubscribeReaction<BattleSetupGA>(reaction, reactionTiming);
    }

    public override void UnsubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.UnsubscribeReaction<BattleSetupGA>(reaction, reactionTiming);
    }
}
