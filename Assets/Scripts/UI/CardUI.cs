using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    //必须保留原来Card的引用,以保证删除
    public Card cardData { get; set; }

    //外部调用监听此事件(小心内存泄漏)
    //NOTE: 现在其会额外传入一个标识符
    public event Action<Card> OnCardSelected;

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
        cardViewImage.color = hoverColor;
        image.color = hoverColor;
    }

    private void HandleClick()
    {
        OnCardSelected?.Invoke(cardData);
        OnCardSelectedInGroup?.Invoke(cardData, idx);
    }

    private void HandleHoverExit()
    {
        cardViewImage.color = originColor;
        image.color = originColor;
    }

    
}
