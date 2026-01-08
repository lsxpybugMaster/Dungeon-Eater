using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCardUI : MonoBehaviour
{
    public CardUI choosenCardUI;
    //2 ~ 3 个卡牌升级选项
    public List<CardUI> updateCardUIList;

    
    private Tween showTween;
    private Vector3 originCardScale;

    //public event Action OnCardUIDeleted;

    //[SerializeField] private Button deleteCardbtn;

    private void Awake()
    {
        originCardScale = choosenCardUI.transform.localScale;
        //deleteCardbtn.onClick.AddListener(Delete);
    }

    private void OnEnable()
    {
        //开始时隐藏
        choosenCardUI.transform.localScale = Vector3.zero;

        foreach (var updateCardUI in updateCardUIList)
        {
            updateCardUI.gameObject.SetActive(false);
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
        //deleteCardbtn.interactable = true;
        choosenCardUI.Setup(card);
        //
        List<CardData> updateChoiceDatas = choosenCardUI.cardData.updateChoices;
        int n = updateChoiceDatas.Count;
        int m = updateCardUIList.Count;
        int i = 0;
        for (; i < n; i++)
        {
            //展示的升级选项是虚拟的卡牌,因此直接构造
            updateCardUIList[i].gameObject.SetActive(true);
            updateCardUIList[i].Setup(new Card(updateChoiceDatas[i]));
        }
        //剩余空白位置隐藏
        for (; i < m; i++)
        {
            updateCardUIList[i].gameObject.SetActive(false);
        }

        ShowCardEffect();
    }

    //private void Delete()
    //{
    //    deleteCardbtn.interactable = false;
    //    DeleteChoosenCard(choosenCardUI.cardData);
    //    DeleteCardEffect();
    //}

    //public void DeleteChoosenCard(Card card)
    //{
    //    GameManager.Instance.PlayerDeckController.RemoveCardFromDeck(card);
    //    // 使用事件通知上层
    //    OnCardUIDeleted?.Invoke();
    //}

    private void ShowCardEffect()
    {
        Debug.Log($"ori_scale {originCardScale}");
        CardScaleEffect(Vector3.zero, originCardScale);
    }

    private void DeleteCardEffect()
    {
        CardScaleEffect(originCardScale, Vector3.zero);
    }

    private void CardScaleEffect(Vector3 fromScale, Vector3 toScale)
    {
        Transform t = choosenCardUI.transform;
        t.localScale = fromScale;

        //防止多次点击叠加 Tween
        showTween?.Kill();

        //播放 Scale 动画
        showTween = t
            .DOScale(toScale, Config.Instance.showCardTime)
            .SetEase(Ease.OutCubic);
    }
}
