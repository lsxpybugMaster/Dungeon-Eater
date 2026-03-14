using System.Collections.Generic;
using UnityEngine;

public enum CardTag
{
    Exhaust, //消耗：使用后立刻从战斗牌库删除
    Status, //状态卡牌
    X,  //消耗 X 资源的卡牌
}

/// <summary>
/// <see cref="CardView">
/// </summary>
public class Card
{
    /// <summary>
    /// 引用型、只读属性 —— 直接引用 SO 数据（浅拷贝）
    /// </summary>
    //编译器不生成隐藏字段,直接从对象获取，适用与不需额外存储进行更新的属性
    public string Title => //data.title; //获取的是本地化字典索引 //data.name;
        GetTitle();
    public string Description => //data.Description;
        LocalizationManager.Instance.Get(data.Description);

    //临时函数,在title为空时返回之前的值
    private string GetTitle()
    {
        if (string.IsNullOrEmpty(data.title))
            return data.name;
        return LocalizationManager.Instance.Get(data.title);
    }

    public Sprite Image => data.Image;

    public Effect ManualTargetEffect => data.ManualTargetEffect;
    public List<AutoTargetEffect> OtherEffects => data.OtherEffects;
    //燃烧,中毒等卡牌的效果
    public List<AutoTargetEffect> CardStatusEffects => data.DiscardEffects;

    public List<CardData> updateChoices => data.UpdateCardInfo;

    public HashSet<CardTag> CardTags;

    // 值型、会在运行时变化 —— 单独存储（深拷贝一份）
    // 需要生成隐藏字段，自动属性
    private int mana;
    public int Mana {
        get => HasTag(CardTag.X) ? ManaSystem.Instance.CurMana : mana; //如果是X卡牌
        set => mana = value;
    }

    //由于引入了X费卡,所以费用数值需要特殊处理
    public string ManaStr => HasTag(CardTag.X) ? "X" : Mana.ToString();

    public ManaID ManaType => data.SpendManaType; 

    public readonly CardData data;

    public Card(CardData cardData)
    {
        data = cardData;      // 只读引用
        mana = cardData.Mana; // 拷贝初始值
        CardTags = new(cardData.CardTags); //将列表转换成哈希集合
    }

    public bool HasTag(CardTag tag) => data.CardTags.Contains(tag);
}
