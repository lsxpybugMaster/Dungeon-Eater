using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    
    [SerializeField] private TMP_Text description;
    
    [SerializeField] private TMP_Text mana;
    
    [SerializeField] private SpriteRenderer imageSR;
    
    [SerializeField] private GameObject wrapper;

    public Card Card { get; private set; }

    /// <summary>
    /// 根据卡牌数据更新信息
    /// </summary>
    /// <param name="card">卡牌数据</param>
    public void Setup(Card card)
    {
        Card = card;
        title.text = card.Title;
        description.text = card.Description;
        mana.text = card.Mana.ToString();
        imageSR.sprite = card.Image;
    }

    //注意要求有碰撞体才能运行
    void OnMouseEnter()
    {
        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2, 0);
        CardViewHoverSystem.Instance.Show(Card, pos);
    }

    void OnMouseExit()
    {
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }

}
