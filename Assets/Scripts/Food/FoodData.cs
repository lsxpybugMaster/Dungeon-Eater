using SerializeReferenceEditor;
using UnityEngine;

public enum FoodType
{
    Perk, //天赋, 即在满足某种条件时触发效果 (被动)
    Immediate, //立即触发效果, 如获取时增加属性, 之后不再起任何作用
}

/// <summary>
/// 道具系统
/// </summary>
[CreateAssetMenu(menuName = "Data/Food")]
public class FoodData : ScriptableObject
{
    //图标
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public FoodType Type { get; private set; }

    //如果是Perk类道具, 需要perkData
    [field: SerializeField] public PerkData perkData;
    [field: SerializeField] public ImmediateData ImmediateData;
}
