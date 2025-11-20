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
    public void Setup(HeroState heroState, GameState gameState, Action onShowDeckBtnClick)
    {
        GetComponents();

        UpdateHeroHp(heroState.CurrentHealth, heroState.MaxHealth);
        UpdateDeckSize(heroState.DeckSize);

        BindButton(onShowDeckBtnClick);

        ResetUp(gameState);
    }

    /// <summary>
    /// 依据场景的切换进行重新初始化(去显示两套数据)
    /// </summary>
    public void ResetUp(GameState gameState)
    {
        SubscribeEventAndInit(gameState);
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

    //订阅事件统一写在这里 + 显式更新
    private void SubscribeEventAndInit(GameState gameState)
    {
        //根据不同模式注册不同事件
        if (gameState == GameState.Exploring)
        {
            Debug.Log("GLO");
            var globalDeck = GameManager.Instance.PlayerDeckController;
            globalDeck.OnDeckSizeChanged -= UpdateDeckSize;
            globalDeck.OnDeckSizeChanged += UpdateDeckSize;
            //绑定全局牌堆
            //显式更新
        }
        else if (gameState == GameState.Battle)
        {
            Debug.Log("BAT");
            var tempDeck = CardSystem.Instance;
            tempDeck.OnBattleDeckChanged -= UpdateDeckSize;
            tempDeck.OnBattleDeckChanged += UpdateDeckSize;
            //绑定战斗牌堆
            //显式更新
        }
        else Debug.Log($"SubscribeEvent 检测到错误的模式: {gameState.ToString()}");

        UpdateDeckSize(GameManager.Instance.HeroState.DeckSize);
    }

    //private void SubscribeEvent(PlayerDeckController playerDeckController)
    //{
    //    // 防御性注册
    //    playerDeckController.OnDeckSizeChanged -= UpdateDeckSize;
    //    playerDeckController.OnDeckSizeChanged += UpdateDeckSize;
    //}
    

    //TODO: 将生命更新后的对应逻辑挂载到ActionSystem中
    public void UpdateHeroHp(int hpAmount, int maxHpAmount)
    {
        heroHpTMP.text = hpAmount.ToString() + "/" + maxHpAmount.ToString();
    }


    /// <param name="size"></param>
    public void UpdateDeckSize(int size)
    {
        deckSizeTMP.text = size.ToString();
    }
}