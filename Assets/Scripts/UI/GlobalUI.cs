using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 跨场景显示的全局UI
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
    /// 初始化基本信息
    /// </summary>
    public void Setup(HeroState heroState)
    {
        UpdateHeroHp(heroState.CurrentHealth, heroState.MaxHealth);
        UpdateDeckSize(heroState.DeckSize);

        SubscribeEvent(heroState);
    }


    //订阅事件统一写在这里
    private void SubscribeEvent(HeroState heroState)
    {
        // 防御性注册
        heroState.OnDeckSizeChanged -= UpdateDeckSize;
        heroState.OnDeckSizeChanged += UpdateDeckSize;
    }

    //TODO: 将生命更新后的对应逻辑挂载到ActionSystem中
    public void UpdateHeroHp(int hpAmount, int maxHpAmount)
    {
        heroHpTMP.text = hpAmount.ToString() + "/" + maxHpAmount.ToString();
    }


    public void UpdateDeckSize(int size)
    {
        deckSizeTMP.text = size.ToString();
    }
}