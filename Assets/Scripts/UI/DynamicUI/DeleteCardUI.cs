using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//删卡UI,提供相关卡牌的显示,管理按钮,以及调用具体接口
//同时管理对卡牌的删除
public class DeleteCardUI : ShowCardUIBase
{
    private CardUI choosenCardUI;

    //private Tween showTween;

    //public event Action OnCardUIDeleted;

    [SerializeField] private Button deleteCardbtn;

    private new void Awake()
    {
        base.Awake();
        choosenCardUI = cardUIPrefab;
        choosenCardUI.transform.localScale = Vector3.zero; //隐藏选中卡牌
        deleteCardbtn.onClick.AddListener(Delete);
    }

    private void OnEnable()
    {
        deleteCardbtn.interactable = false;
    }

    //建立卡牌UI点击与调用的联系
    //public void RegistCardUI(CardUI cardUI)
    //{
    //    cardUI.OnCardSelected += ShowChoosenCard;
    //}

    public override void ShowChoosenCard(Card card)
    {
        deleteCardbtn.interactable = true;
        choosenCardUI.Setup(card);
        ShowCardUIEffect(choosenCardUI.transform);
    }

    private void Delete()
    {
        if (AvailableTimes <= 0)
            return;

        AvailableTimes--;

        deleteCardbtn.interactable = false;
        DeleteChoosenCard(choosenCardUI.cardData);
        HideCardUIEffect(choosenCardUI.transform);
        // DeleteCardEffect();
    }

    public void DeleteChoosenCard(Card card)
    {
        GameManager.Instance.PlayerDeckController.RemoveCardFromDeck(card);
        // 使用事件通知上层
        //OnCardUIDeleted?.Invoke();
        InvokeOnCardUIClicked(); //基类调用事件
    }

    //private void ShowCardEffect()
    //{
    //    CardScaleEffect(Vector3.zero, Vector3.one);
    //}

    //private void DeleteCardEffect()
    //{
    //    CardScaleEffect(Vector3.one, Vector3.zero);
    //}

    //private void CardScaleEffect(Vector3 fromScale, Vector3 toScale)
    //{
    //    Transform t = choosenCardUI.transform;
    //    t.localScale = fromScale;

    //    //防止多次点击叠加 Tween
    //    showTween?.Kill();

    //    //播放 Scale 动画
    //    showTween = t
    //        .DOScale(toScale, Config.Instance.showCardTime)
    //        .SetEase(Ease.OutCubic);
    //}
}
