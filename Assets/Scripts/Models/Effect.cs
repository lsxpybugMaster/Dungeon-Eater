using System.Collections.Generic;

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
