using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCardUI : ShowCardUIBase
{
    //[SerializeField] CardUI choosenCardUI;
    private CardUI choosenCardUI;
    //2 ~ 3 个卡牌升级选项
    [SerializeField] List<CardUI> updateCardUIList;
    [SerializeField] List<Button> buttonList;
    
    // private Tween showTween;
    // private Vector3 originCardScale;

    public event Action OnCardUIUpdated;

    //[SerializeField] private Button deleteCardbtn;

    private new void Awake()
    {
        base.Awake();

        choosenCardUI = cardUIPrefab; //重新命名
        //originCardScale = choosenCardUI.transform.localScale;
        //deleteCardbtn.onClick.AddListener(Delete);

        //初始化按钮对应返回的值,注意闭包问题
        for (int i = 0; i < updateCardUIList.Count; i++)
        {
            int locali = i;
            buttonList[i].onClick.AddListener(() => { UpdateCard(locali); });
        }
    }

    private void OnEnable()
    {
        //开始时隐藏
        choosenCardUI.transform.localScale = Vector3.zero;

        for(int i = 0 ; i < updateCardUIList.Count; i++)
        {
            updateCardUIList[i].gameObject.SetActive(false);
            buttonList[i].gameObject.SetActive(false);
        }
        //deleteCardbtn.interactable = false;
    }

    //建立卡牌UI点击与调用的联系
    public void RegistCardUI(CardUI cardUI)
    {
        cardUI.OnCardSelected += ShowChoosenCard;
    }

    public void ShowChoosenCard(Card card)
    {
        if (!gameObject.activeInHierarchy)
        {
            //C# 事件注册/回调与Unity无关, 所以需要对象自己控制
            Debug.Log($" {this.name} 脚本已被禁用,事件执行终止");
            return;
        }

        //deleteCardbtn.interactable = true;
        choosenCardUI.Setup(card);
        //
        List<CardData> updateChoiceDatas = choosenCardUI.cardData.updateChoices;
        int n = updateChoiceDatas.Count;
        int m = updateCardUIList.Count;
        int i = 0;
        for (; i < n; i++)
        {
            var updateCardUI = updateCardUIList[i];
            //展示的升级选项是虚拟的卡牌,因此直接构造       
            buttonList[i].gameObject.SetActive(true);
            updateCardUI.gameObject.SetActive(true);
            updateCardUI.Setup(new Card(updateChoiceDatas[i]));

            ShowCardUIEffect(updateCardUI.transform);
        }
        //剩余空白位置隐藏
        for (; i < m; i++)
        {
            updateCardUIList[i].gameObject.SetActive(false);
            buttonList[i].gameObject.SetActive(false);
        }

        ShowCardUIEffect(choosenCardUI.transform);
    }


    /// <summary>
    /// 由按钮传入索引,在依据索引执行对应功能
    /// </summary>
    /// <param name="i"></param>
    public void UpdateCard(int i)
    {
        Card origin = choosenCardUI.cardData;
        Card target = updateCardUIList[i].cardData;

        GameManager.Instance.PlayerDeckController.UpdateCardFromDeck(origin, target);

        //及时处理信息
        HideAllButtons(); //防止又一次升级

        //从视觉上隐藏UI
        HideAllCards();

        OnCardUIUpdated?.Invoke();
    }

    //private void ShowCardEffect(Transform t)
    //{
    //    AnimUtil.CardScaleAnim(t, Vector3.zero, originCardScale, Config.Instance.showCardTime);
    //}

    //private void HideCardUIEffect(Transform t)
    //{
    //    AnimUtil.CardScaleAnim(t, originCardScale, Vector3.zero, Config.Instance.showCardTime);
    //}

    private void HideAllButtons()
    {
        foreach (var btn in buttonList)
        {
            btn.gameObject.SetActive(false);
        }
    }

    private void HideAllCards()
    {
        HideCardUIEffect(choosenCardUI.transform);
        foreach (var ui in updateCardUIList)
        {
            //被隐藏了无所谓
            HideCardUIEffect(ui.transform);
        }
    }
}
