using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//TODO: ������CardView����,�Ƿ�ɸ��ã�
public class CardUI : MonoBehaviour
{
    //��ҪUIչʾ������
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
