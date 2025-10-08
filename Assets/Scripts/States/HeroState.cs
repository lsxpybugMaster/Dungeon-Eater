using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 跨场景持久化的数据, 最小化维护开销
/// </summary>
[System.Serializable]
public class HeroState
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    //仅初始化一次初始数据
    public void Init(HeroData data)
    {
        MaxHealth = data.Health;
        CurrentHealth = data.Health;
    }

    //及时接受更新的临时数据
    public void Save(int currentHealth, int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }
}
