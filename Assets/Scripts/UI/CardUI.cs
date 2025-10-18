using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TODO: 这里与CardView类似,是否可复用？
public class CardUI : MonoBehaviour
{
    //需要UI展示的属性
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text mana;
    [SerializeField] private SpriteRenderer imageSR;

    public void Setup(Card card)
    {
        title.text = card.Title;
        description.text = card.Description;
        mana.text = card.Mana.ToString();        
    }
}
