using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Card")]
public class CardData : ScriptableObject
{
    /*
        区分属性与字段
        属性对字段的get,set进行包装，调用时正常
        自动属性只需声明get, 由编译器生成隐藏字段
     */
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Mana { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
}
