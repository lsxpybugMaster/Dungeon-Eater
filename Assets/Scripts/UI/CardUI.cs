using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//在UI中以卡牌形式展示卡牌数据
//TODO: 这里与CardView类似,是否可复用？
public class CardUI : MonoBehaviour
{
    //控制投射的组件
    [SerializeField] private UIRayCastArea uiRayCastArea;

    //需要UI展示的属性
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text mana;
    [SerializeField] private Image image;
    [SerializeField] private Color hoverColor;

    private Color originColor;
    private Image cardViewImage;
    public Vector3 oriScale {get; set;}

    //必须保留原来Card的引用,以保证删除
    public Card cardData { get; set; }

    //外部调用监听此事件(小心内存泄漏)
    public event Action<Card> OnCardSelected;

    //扩展事件,用于有一系列CardUI对象的情况
    private int idx = 0;
    public event Action<Card, int> OnCardSelectedInGroup;

    private void OnDestroy()
    {
        OnCardSelected = null;
        OnCardSelectedInGroup = null;
    }


    private void Awake()
    {
        if (uiRayCastArea != null)
        {
            uiRayCastArea.OnHoverEnter += HandleHoverEnter;
            uiRayCastArea.OnHoverExit += HandleHoverExit;
            uiRayCastArea.OnClick += HandleClick;
        }

        oriScale = transform.localScale;
    }

    //取消该卡牌的交互功能
    public void DisableCasting()
    {
        uiRayCastArea.OnHoverEnter -= HandleHoverEnter;
        uiRayCastArea.OnHoverExit -= HandleHoverExit;
        uiRayCastArea.OnClick -= HandleClick;
    }


    //将作为一组CardUI中的一个,比正常的SetUp复杂些
    public void SetupForGroup(Card card, int idx)
    {
        Setup(card);
        this.idx = idx; 
    }


    public void Setup(Card card)
    {
        originColor = image.color;
        cardViewImage = GetComponent<Image>();
        title.text = card.Title;
        description.text = card.Description;
        mana.text = card.Mana.ToString();        
        image.sprite = card.Image;

        cardData = card;
    }

    private void HandleHoverEnter()
    {
        MouseEnterEffect();
    }

    private void HandleClick()
    {
        OnCardSelected?.Invoke(cardData);
        OnCardSelectedInGroup?.Invoke(cardData, idx);
    }

    private void HandleHoverExit()
    {
        MouseExitEffect();
    }

    public void MouseEnterEffect()
    {
        cardViewImage.color = hoverColor;
        image.color = hoverColor;

        transform.DOScale(oriScale * 1.05f, 0.15f)
                 .SetEase(Ease.OutBack);
    }

    public void MouseExitEffect()
    {
        cardViewImage.color = originColor;
        image.color = originColor;

        transform.DOScale(oriScale, 0.15f)
                 .SetEase(Ease.OutQuad);
    }
}
