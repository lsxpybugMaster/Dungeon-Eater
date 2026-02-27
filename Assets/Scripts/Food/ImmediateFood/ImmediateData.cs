using System;

[Serializable]
public abstract class ImmediateData
{
    /// <summary>
    /// 后面的逻辑需要在OnPickup里实现, 以便在拾取时触发效果
    /// </summary>
    public abstract void OnPickup();
}

/*
    直接通过SerializeReference序列化, 以便在FoodData中直接编辑
    既然无法通过Effect来动态配置, 那么不再使用SO
*/


/// <summary>
/// 该道具在拾取时直接增加金钱, 之后不再起作用
/// </summary>
public class Imm_GetMoney : ImmediateData
{
    public int moneyAmount; //获得的金钱数量

    public override void OnPickup()
    {
        GameManager.Instance.HeroState.GainCoins(moneyAmount);
    }
}

/// <summary>
/// 该道具在拾取时直接增加最大法力值, 之后不再起作用
/// </summary>
public class Imm_AddMaxMana : ImmediateData
{
    public int manaAmount;

    public override void OnPickup()
    {
        GameManager.Instance.HeroState.AddMaxMana(manaAmount);
    }
}
