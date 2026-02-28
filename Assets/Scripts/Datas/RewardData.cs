using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType
{
    GOLD,
    LIFEHEAL,
    FOOD,
    CARDCHOOSE,
}


/// <summary>
/// 奖励类型, 依据RewardType进行索引
/// </summary>
[CreateAssetMenu(menuName = "Data/Reward")]
public class RewardData : ScriptableObject, IHaveKey<RewardType>
{
    [field: SerializeField] public RewardType RewardType { get; set; }
    public RewardType GetKey() => RewardType;

    //小图标信息
    [field: SerializeField] public string SmallIcon { get; set; }

    [Header("描述模板, 使用{ }来包含想要模板化的部分并添加变量名")]
    [SerializeField] private string descriptionTemplate;
    [Header("指定变量名")]
    [SerializeField] private string param;

    [field: SerializeField] public string Title { get; set; }

    [field: SerializeField] public Sprite Image { get; set; }

    public string GetDescription(int num1)
    {
        return descriptionTemplate.FormatNamed(
            new Dictionary<string, object> {
                { param, num1 }
            }
        );
    }    
}
