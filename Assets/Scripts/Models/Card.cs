using System.Collections.Generic;
using UnityEngine;

public enum CardTag
{
    Exhaust, //消耗：使用后立刻从战斗牌库删除
}

public class Card
{
    /// <summary>
    /// 引用型、只读属性 —— 直接引用 SO 数据（浅拷贝）
    /// </summary>
    //编译器不生成隐藏字段,直接从对象获取，适用与不需额外存储进行更新的属性
    public string Title => data.name;
    public string Description => data.Description;
    public Sprite Image => data.Image;
    public Effect ManualTargetEffect => data.ManualTargetEffect;
    public List<AutoTargetEffect> OtherEffects => data.OtherEffects;
    public List<CardData> updateChoices => data.UpdateCardInfo;

    public HashSet<CardTag> CardTags;

    // 值型、会在运行时变化 —— 单独存储（深拷贝一份）
    // 需要生成隐藏字段，自动属性
    public int Mana { get; private set;}

    public readonly CardData data;

    public Card(CardData cardData)
    {
        data = cardData;      // 只读引用
        Mana = cardData.Mana; // 拷贝初始值
        CardTags = new(cardData.CardTags); //将列表转换成哈希集合
    }

    public bool HasTag(CardTag tag) => data.CardTags.Contains(tag);
}
