using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// DataBase配置<Type,Class> => Class 存储相关函数以及数值

/// <summary>
/// 全局状态配置库：
/// 负责管理所有 CardData 的查找、缓存与随机获取。
/// 对外提供只读接口，禁止外部直接修改或访问底层列表。
/// </summary>
[CreateAssetMenu(menuName = "DataBase/StatusEffectDatabase")]
public class StatusEffectDataBase : ScriptableObject
{
    [SerializeField] private List<StatusEffect> allEffects = new();
}
