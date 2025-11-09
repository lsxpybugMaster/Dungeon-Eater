using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 顶部的UI
/// </summary>
public class TopUI : MonoBehaviour, IAmPersistUI
{
    [SerializeField] private TMP_Text heroHpTMP;
    [SerializeField] private TMP_Text deckSizeTMP;
    //TODO: 调试用,最后删除
    [SerializeField] private TMP_Text debugTMP;
    [SerializeField] private Button showDeckBtn;

    private void Start()
    {
     
    }

    private void Update()
    {
        //TODO: 调试用,最后删除
        debugTMP.text = GameManager.Instance.GameState.ToString();
    }

    /// <summary>
    /// 初始化基本信息
    /// </summary>
    public void Setup(HeroState heroState, PlayerDeckController playerDeckController, Action onShowDeckBtnClick)
    {
        GetComponents();

        UpdateHeroHp(heroState.CurrentHealth, heroState.MaxHealth);
        UpdateDeckSize(heroState.DeckSize);

        SubscribeEvent(playerDeckController);

        BindButton(onShowDeckBtnClick);
    }


    //传入函数委托并绑定按钮
    public void BindButton(Action onShowDeckBtnClick)
    {
        showDeckBtn.onClick.RemoveAllListeners();
        if (onShowDeckBtnClick != null)
        {
            showDeckBtn.onClick.AddListener(() => onShowDeckBtnClick());
        }
    }

    private void GetComponents()
    {
        showDeckBtn.GetComponent<Button>();
    }

    //订阅事件统一写在这里
    private void SubscribeEvent(PlayerDeckController playerDeckController)
    {
        // 防御性注册
        playerDeckController.OnDeckSizeChanged -= UpdateDeckSize;
        playerDeckController.OnDeckSizeChanged += UpdateDeckSize;
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