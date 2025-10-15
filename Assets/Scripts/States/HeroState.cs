using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �糡���־û�������, ��С��ά������
/// </summary>
[System.Serializable]
public class HeroState
{
    //DISCUSS: Ϊ����ȫ��װHeroData,����ʹ��HeroState����HeroData�Ĳ������ݲ���,ȷ������ϵͳ��֪��HeroData����
    public HeroData BaseData {  get; private set; }

    //------------------------��̬����---------------------------
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public List<CardData> Deck { get; private set; }

    //------------------------�־û�����---------------------------
    public Sprite HeroSprite => BaseData.Image;


    //����ʼ��һ�γ�ʼ����
    public void Init(HeroData data)
    {
        BaseData = data;

        MaxHealth = data.Health;
        CurrentHealth = data.Health;
        Deck = data.Deck;
    }


    //��ʱ���ܸ��µ���ʱ����
    public void Save(int currentHealth, int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }
}
