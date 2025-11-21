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
    public void Setup(HeroState heroState, Action onShowDeckBtnClick)
    {
        GetComponents();

        UpdateHeroHp(heroState.CurrentHealth, heroState.MaxHealth);
        UpdateDeckSize(heroState.DeckSize);

        BindButton(onShowDeckBtnClick);
    }

    /// <summary>
    /// 依据场景的切换进行重新初始化(去显示两套数据)
    /// </summary>
    public void ResetUp()
    {
        //如果回到了探索场景需要刷新UI
        if (GameManager.Instance.GameState == GameState.Exploring)
        {
            UpdateDeckSize(GameManager.Instance.PlayerDeckController.DeckSize);
        }
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