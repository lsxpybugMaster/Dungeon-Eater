using SerializeReferenceEditor;
using UnityEditor;
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
public class FoodData : ScriptableObject, IShopItem, IHaveKey<string>
{
    [field: SerializeField] public string Id { get; private set; } //唯一标识符

    public string GetKey() => Id; //其作为数据库索引

    //图标
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public FoodType Type { get; private set; }
    [field: SerializeField] public int price { get; private set; }

    public int BasePrice => price;

    //如果是Perk类道具, 需要perkData
    [field: SerializeField] public PerkData perkData;
    [field: SerializeField] public ImmediateData ImmediateData;


#if UNITY_EDITOR
    // Unity 的编辑器回调方法
    private void OnValidate()
    {
        // 使用文件名作为ID
        string assetPath = AssetDatabase.GetAssetPath(this);
        if (!string.IsNullOrEmpty(assetPath))
        {
            // 直接使用文件名（不含扩展名）作为ID
            Id = System.IO.Path.GetFileNameWithoutExtension(assetPath);
            EditorUtility.SetDirty(this);
        }
    }
#endif
}
