using System.Collections.Generic;
using UnityEngine;
public class Card
{
    //编译器不生成隐藏字段,直接从对象获取，适用与不需额外存储进行更新的属性
    public string Title => data.name;
    public string Description => data.Description;
    public Sprite Image => data.Image;
    public List<Effect> Effects => data.Effects;

    //需要生成隐藏字段，自动属性
    public int Mana { get; private set;}

    private readonly CardData data;

    public Card(CardData cardData)
    {
        data = cardData;
        Mana = cardData.Mana;
    }
}
