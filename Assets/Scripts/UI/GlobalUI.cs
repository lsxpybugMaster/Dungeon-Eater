using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// �糡����ʾ��ȫ��UI
/// </summary>
public class GlobalUI : PersistentSingleton<GlobalUI>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text heroHpTMP;
    [SerializeField] private TMP_Text deckSizeTMP;

    private void Start()
    {

    }

    /// <summary>
    /// ��ʼ��������Ϣ
    /// </summary>
    public void Setup(HeroState heroState)
    {
        UpdateHeroHp(heroState.CurrentHealth, heroState.MaxHealth);
        UpdateDeckSize(heroState.DeckSize);

        SubscribeEvent(heroState);
    }


    //�����¼�ͳһд������
    private void SubscribeEvent(HeroState heroState)
    {
        // ������ע��
        heroState.OnDeckSizeChanged -= UpdateDeckSize;
        heroState.OnDeckSizeChanged += UpdateDeckSize;
    }

    //TODO: ���������º�Ķ�Ӧ�߼����ص�ActionSystem��
    public void UpdateHeroHp(int hpAmount, int maxHpAmount)
    {
        heroHpTMP.text = hpAmount.ToString() + "/" + maxHpAmount.ToString();
    }


    public void UpdateDeckSize(int size)
    {
        deckSizeTMP.text = size.ToString();
    }
}