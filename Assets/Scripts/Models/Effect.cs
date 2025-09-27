using System.Collections.Generic;

[System.Serializable]
public abstract class Effect
{
    //效果对象能够执行GA
    /// <summary>
    /// 将效果转换为GameAction
    /// </summary>
    /// <returns></returns>
    public abstract GameAction GetGameAction(List<CombatantView> targets);
}
