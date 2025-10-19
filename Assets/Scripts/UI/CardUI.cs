using TMPro;
using UnityEngine;
using UnityEngine.UI;

//在UI中以卡牌形式展示卡牌数据
//TODO: 这里与CardView类似,是否可复用？
public class CardUI : MonoBehaviour
{
    //需要UI展示的属性
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
