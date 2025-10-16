using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �糡���־û�������, ��С��ά������
/// ��ʵ���ϵ����ȫ������
/// </summary>
[System.Serializable]
public class HeroState
{
    //DISCUSS: Ϊ����ȫ��װHeroData,����ʹ��HeroState����HeroData�Ĳ������ݲ���,ȷ������ϵͳ��֪��HeroData����
    public HeroData BaseData {  get; private set; }

    //------------------------���ݸ����¼�---------------------------
    public event Action<int> OnDeckSizeChanged;

    //------------------------��̬����---------------------------
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    /// <summary>
    /// ��Ҿ��⿨����Ϣ�ڴ�
    /// </summary>
    public List<CardData> Deck { get; private set; }

    //------------------------�־û�����---------------------------
    public Sprite HeroSprite => BaseData.Image;

    public int DeckSize => Deck.Count;


    //����ʼ��һ�γ�ʼ����
    public void Init(HeroData heroData)
    {
        BaseData = heroData;

        MaxHealth = heroData.Health;
        CurrentHealth = heroData.Health;
        Deck = heroData.Deck;
    }

    //��ʱ���ܸ��µ���ʱ����
    public void Save(int currentHealth, int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;
    }


    /// <summary>
    /// ��������ӿ���
    /// </summary>
    public void AddCardToDeck(CardData cardData)
    {
        OnDeckSizeChanged?.Invoke(DeckSize);
    }


    /// <summary>
    /// ������ɾ������
    /// </summary>
    public void RemoveCardFromDeck(CardData cardData)
    {
        OnDeckSizeChanged?.Invoke(DeckSize);
    }

}
