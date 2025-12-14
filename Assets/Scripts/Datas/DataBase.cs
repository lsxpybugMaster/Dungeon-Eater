using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataBase<TKey, TValue> : ScriptableObject where TValue : class, IHaveKey<TKey>
{
    // 外部配置卡牌总数据
    [SerializeField] protected List<TValue> datas = new();

    // 索引卡牌
    protected Dictionary<TKey, TValue> idLookup;

    // --------------------- 初始化 ---------------------
    protected void Init()
    {
        if (idLookup != null) return; // 避免重复初始化

        idLookup = new Dictionary<TKey, TValue>();
        //不考虑大小写的查找

        foreach (var d in datas)
        {
            if (d == null) continue;

            TKey key = d.GetKey(); //利用接口 + 泛型约束

            if (idLookup.ContainsKey(key))
                Debug.LogWarning($"[{GetType().Name}] Duplicate ID detected: {key}");
            else
                idLookup.Add(key, d);
        }

        Debug.Log($"[{GetType().Name}] Initialized with {datas.Count} data.");
    }
}
