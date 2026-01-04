using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;

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
    //TODO: 卡牌键
    private Card cardData;

    //TODO: 重构通信方式
    public Action<Card> OnCardChoosen { get; set; }

    private void Awake()
    {
        if (uiRayCastArea != null)
        {
            uiRayCastArea.OnHoverEnter += HandleHoverEnter;
            uiRayCastArea.OnHoverExit += HandleHoverExit;
            uiRayCastArea.OnClick += HandleClick;
        }
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

    public void HandleHoverEnter()
    {
        cardViewImage.color = hoverColor;
        image.color = hoverColor;
        Debug.Log("HandleHoverEnter");
    }

    public void HandleClick()
    {
        OnCardChoosen(cardData);
        Debug.Log("HandleClick");
    }

    public void HandleHoverExit()
    {
        cardViewImage.color = originColor;
        image.color = originColor;

        Debug.Log("HandleHoverExit");
    }

}
