using TMPro;
using UnityEngine;
using UnityEngine.UI;

//��UI���Կ�����ʽչʾ��������
//TODO: ������CardView����,�Ƿ�ɸ��ã�
public class CardUI : MonoBehaviour
{
    //��ҪUIչʾ������
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text mana;
    [SerializeField] private Image image;

    public void Setup(Card card)
    {
        title.text = card.Title;
        description.text = card.Description;
        mana.text = card.Mana.ToString();        
        image.sprite = card.Image;
    }
}
