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
    /// </summary>
    /// <returns></returns>
    public abstract GameAction GetGameAction(List<CombatantView> targets, CombatantView caster);
}
