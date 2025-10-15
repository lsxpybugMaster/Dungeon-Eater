using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 跨场景持久化的数据, 最小化维护开销
/// </summary>
[System.Serializable]
public class HeroState
{
    //DISCUSS: 为了完全封装HeroData,我们使用HeroState管理HeroData的不变数据部分,确保其他系统仅知道HeroData存在
    public HeroData BaseData {  get; private set; }

    //------------------------动态数据---------------------------
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public List<CardData> Deck { get; private set; }

    //------------------------持久化数据---------------------------
    public Sprite HeroSprite => BaseData.Image;


    //仅初始化一次初始数据
    public void Init(HeroData data)
    {
        BaseData = data;

        MaxHealth = data.Health;
        CurrentHealth = data.Health;
        Deck = data.Deck;
    }


    //及时接受更新的临时数据
    public void Save(int currentHealth, int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }
}
