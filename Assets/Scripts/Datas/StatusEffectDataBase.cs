using System;
using System.Collections.Generic;
using UnityEngine;
// DataBase配置<Type,Class> => Class 存储相关函数以及数值

/// <summary>
/// 全局状态配置库：
/// 负责管理所有 CardData 的查找、缓存与随机获取。
/// 对外提供只读接口，禁止外部直接修改或访问底层列表。
//TODO: 优化 DataBase, 使用公共基类描述
/// </summary>
[CreateAssetMenu(menuName = "DataBase/StatusEffectDatabase")]
public class StatusEffectDataBase : ScriptableObject
{
    [SerializeField] private List<StatusEffect> allEffects = new();

    private Dictionary<StatusEffectType, StatusEffect> typeLookup;


    // --------------------- 初始化 ---------------------
    public void Init()
    {
        if (typeLookup != null) return; // 避免重复初始化

        typeLookup = new();

        foreach (var effect in allEffects)
        {
            if (effect == null) continue;

            if (typeLookup.ContainsKey(effect.Type))
                Debug.LogWarning($"[StatusEffectDataBase] Duplicate EffectType detected: {effect.Type}");
            else
                typeLookup.Add(effect.Type, effect);
        }

        Debug.Log($"[StatusEffectDataBase] Initialized with {typeLookup.Count} effects.");
    }

    // --------------------- 单例访问 ---------------------
    private static StatusEffectDataBase _instance;
    public static StatusEffectDataBase I
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<StatusEffectDataBase>("StatusEffectDataBase");
                if (_instance == null)
                    Debug.LogError("StatusEffectDataBase.asset not found in Resources folder!");
                else
                    _instance.Init();
            }
            return _instance;
        }
    }

    // --------------------- 查询接口 ---------------------

    /// <summary> 通过卡牌ID查找模板 </summary>
    public static StatusEffect GetEffect(StatusEffectType effectType)
    {
        return I.typeLookup.TryGetValue(effectType, out var e)
            ? e
            : null;
    }

}
