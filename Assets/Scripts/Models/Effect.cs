using System;
using System.Collections.Generic;

/*
    将GameAction + 外部参数封装并暴露给编辑器
    Effect 可以用于在外部配置GA
    可以应用于卡牌的数据配置
    Intend同理
 */
[System.Serializable]
public abstract class Effect
{
    //效果对象能够执行GA
    /// <summary>
    /// 将效果转换为GameAction, 同时指明发起者Caster
    /// Caster有可能是通过上层的AutoTargetEffect中TargetMode决定,Effect无需知晓
    /// 底层的子系统中Effect不一定完全使用其中的参数!
    /// </summary>
    /// <returns></returns>
    public abstract GameAction GetGameAction(List<CombatantView> targets, CombatantView caster, EffectContext context);
}

/// <summary>
/// 决定Effect效果,用于与同一张卡牌后续效果
/// </summary>
public class EffectContext
{
    public bool MainEffectSuccess { get; private set; } = true; //一定默认为true!

    public void SetMainEffectSuccess(Result res)
    {
        if (res == Result.Success || res == Result.GiantSuccess) 
        {
            MainEffectSuccess = true;
        }
        else if (res == Result.GiantFail || res == Result.Failure)
        {
            MainEffectSuccess = false;
        }
    }
}