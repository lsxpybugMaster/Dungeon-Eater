using UnityEngine;
using UnityEngine.UI;

public class PerkUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public Perk Perk {  get; private set; }

    //使用Perk数据初始化
    public void Setup(Perk perk)
    {
        Perk = perk;
        image.sprite = perk.Image;
    }
}
