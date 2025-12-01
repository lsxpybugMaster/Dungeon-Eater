using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HeroData的数据一定不能改变,其用于初始化HeroState在由HeroState处理改变
/// </summary>
[CreateAssetMenu(menuName = "Data/Hero")]
public class HeroData : CombatantData
{
    //[field: SerializeField] public Sprite Image {  get; private set; }
    //[field: SerializeField] public int Health { get; private set; }
    //给每个英雄其自己的卡组
    [field: SerializeField] public List<CardData> Deck {  get; private set; }

}
