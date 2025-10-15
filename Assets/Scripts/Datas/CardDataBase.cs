using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ȫ�ֿ������ݿ⣺
/// ����������� CardData �Ĳ��ҡ������������ȡ��
/// �����ṩֻ���ӿڣ���ֹ�ⲿֱ���޸Ļ���ʵײ��б�
/// </summary>
[CreateAssetMenu(menuName = "Database/CardDatabase")]
public class CardDatabase : ScriptableObject
{
    // �ⲿ���ÿ���������
    [SerializeField] private List<CardData> allCards = new();

    // ��������
    private Dictionary<string, CardData> idLookup;

    // --------------------- ��ʼ�� ---------------------
    public void Init()
    {
        if (idLookup != null) return; // �����ظ���ʼ��

        idLookup = new Dictionary<string, CardData>();
        //�����Ǵ�Сд�Ĳ���

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

    // --------------------- �������� ---------------------
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

    // --------------------- ��ѯ�ӿ� ---------------------

    /// <summary> ͨ������ID����ģ�� </summary>
    public static CardData GetById(string id)
    {
        return Instance.idLookup.TryGetValue(id, out var card)
            ? card
            : null;
    }


    // --------------------- ����ӿ� ---------------------

    /// <summary> �����ȡһ�ſ��� </summary>
    /// ����ĺ�����ʽ bool func(CardData)
    public static CardData GetRandomCard(Func<CardData, bool> filter = null)
    {
        //ȷ������ķ�Χ
        var pool = filter == null ? Instance.allCards : Instance.allCards.Where(filter).ToList();
        if (pool.Count == 0) return null;

        int index = UnityEngine.Random.Range(0, pool.Count);
        return pool[index];
    }


    /// <summary> ��ȡ���п��ƣ�ֻ���� </summary>
    public static IReadOnlyList<CardData> AllCards => Instance.allCards;
}
