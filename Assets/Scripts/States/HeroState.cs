using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �糡���־û�������, ��С��ά������
/// </summary>
[System.Serializable]
public class HeroState
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    //����ʼ��һ�γ�ʼ����
    public void Init(HeroData data)
    {
        MaxHealth = data.Health;
        CurrentHealth = data.Health;
    }

    //��ʱ���ܸ��µ���ʱ����
    public void Save(int currentHealth, int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }
}
