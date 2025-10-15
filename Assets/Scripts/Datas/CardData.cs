using SerializeReferenceEditor;
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

    [field: SerializeField] public string Id { get; private set; } //唯一标识符
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Mana { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    //卡牌功能分解为Effect (使用了第三方库以在编辑器中显示)

    //手动操作的Effect仅有一个
    [field: SerializeReference, SR] public Effect ManualTargetEffect { get; private set; } = null;
    //有许多自动决定目标对象的Effect
    [field: SerializeField] public List<AutoTargetEffect> OtherEffects { get; private set; }


#if UNITY_EDITOR
    // Unity 的编辑器回调方法
    private void OnValidate()
    {
        // 如果还没有ID，则自动生成（防止重复）
        if (string.IsNullOrEmpty(Id))
            Id = System.Guid.NewGuid().ToString();
    }
#endif
}
