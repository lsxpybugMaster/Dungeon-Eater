using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 全局卡牌数据库：
/// 负责管理所有 CardData 的查找、缓存与随机获取。
/// 对外提供只读接口，禁止外部直接修改或访问底层列表。
/// </summary>
[CreateAssetMenu(menuName = "DataBase/CardDatabase")]
public class CardDatabase : ScriptableObject
{
    // 外部配置卡牌总数据
    [SerializeField] private List<CardData> allCards = new();

    // 索引卡牌
    private Dictionary<string, CardData> idLookup;

    // --------------------- 初始化 ---------------------
    public void Init()
    {
        if (idLookup != null) return; // 避免重复初始化

        idLookup = new Dictionary<string, CardData>();
        //不考虑大小写的查找

        foreach (var card in allCards)
        {
            if (card == null) continue;

            if (idLookup.ContainsKey(card.Id))
                Debug.LogWarning($"[CardDatabase] Duplicate Card ID detected: {card.Id}");
            else
                idLookup.Add(card.Id, card);   
        }

        Debug.Log($"[CardDatabase] Initialized with {allCards.Count} cards.");
    }

    // --------------------- 单例访问 ---------------------
    private static CardDatabase _instance;
    public static CardDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<CardDatabase>("CardDatabase");
                if (_instance == null)
                    Debug.LogError("CardDatabase.asset not found in Resources folder!");
                else
                    _instance.Init();
            }
            return _instance;
        }
    }

    // --------------------- 查询接口 ---------------------

    /// <summary> 通过卡牌ID查找模板 </summary>
    public static CardData GetById(string id)
    {
        return Instance.idLookup.TryGetValue(id, out var card)
            ? card
            : null;
    }


    // --------------------- 随机接口 ---------------------

    /// <summary> 随机抽取一张卡牌 </summary>
    /// 传入的函数形式 bool func(CardData)
    public static CardData GetRandomCard(Func<CardData, bool> filter = null)
    {
        //确定随机的范围
        var pool = filter == null ? Instance.allCards : Instance.allCards.Where(filter).ToList();
        if (pool.Count == 0) return null;

        int index = UnityEngine.Random.Range(0, pool.Count);
        return pool[index];
    }


    /// <summary> 获取所有卡牌（只读） </summary>
    public static IReadOnlyList<CardData> AllCards => Instance.allCards;
}
