using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//删卡UI,提供相关卡牌的显示,管理按钮,以及调用具体接口
//同时管理对卡牌的删除
public class DeleteCardUI : MonoBehaviour
{
    public CardUI choosenCardUI;

    private Tween showTween;

    public event Action OnCardUIDeleted;

    [SerializeField] private Button deleteCardbtn;

    private void Awake()
    {
        deleteCardbtn.onClick.AddListener(Delete);
    }

    //建立卡牌UI点击与调用的联系
    public void RegistCardUI(CardUI cardUI)
    {
        cardUI.OnCardSelected += ShowChoosenCard;
    }

    public void ShowChoosenCard(Card card)
    {
        choosenCardUI.Setup(card);

        ShowCardEffect();
    }

    private void Delete()
    {
        DeleteChoosenCard(choosenCardUI.cardData);
    }

    public void DeleteChoosenCard(Card card)
    {
        GameManager.Instance.PlayerDeckController.RemoveCardFromDeck(card);
        // 使用事件通知上层
        // OnCardUIDeleted?.Invoke();
    }

    private void ShowCardEffect()
    {
        Transform t = choosenCardUI.transform;
        t.localScale = Vector3.zero;

        //防止多次点击叠加 Tween
        showTween?.Kill();

        //播放 Scale 动画
        showTween = t
            .DOScale(Vector3.one, Config.Instance.showCardTime)
            .SetEase(Ease.OutCubic);
    }
}
