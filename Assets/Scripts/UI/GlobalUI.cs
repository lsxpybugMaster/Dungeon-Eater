using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// �糡����ʾ��ȫ��UI
/// </summary>
//OPTIMIZE: ��ǰ�ǿ糡������,������GameManager�����־û�
public class GlobalUI : MonoBehaviour 
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
    public void Setup(HeroState heroState, PlayerDeckController playerDeckController)
    {
        UpdateHeroHp(heroState.CurrentHealth, heroState.MaxHealth);
        UpdateDeckSize(heroState.DeckSize);

        SubscribeEvent(playerDeckController);
    }


    //�����¼�ͳһд������
    private void SubscribeEvent(PlayerDeckController playerDeckController)
    {
        // ������ע��
        playerDeckController.OnDeckSizeChanged -= UpdateDeckSize;
        playerDeckController.OnDeckSizeChanged += UpdateDeckSize;
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