using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于处理玩家弃牌阶段的状态卡牌的执行
/// </summary>
public class ApplyCardDiscardedEffectGA : GameAction
{
    public int Damage { get; private set; }
    public StatusEffectType CardStateEffectType { get; private set; }
    //对应的效果
    public GameObject VFX { get; private set; }

    public ApplyCardDiscardedEffectGA(int damage, StatusEffectType cardStateEffectType, GameObject vFX)
    {
        Damage = damage;
        CardStateEffectType = cardStateEffectType;
        VFX = vFX;
    }
}
