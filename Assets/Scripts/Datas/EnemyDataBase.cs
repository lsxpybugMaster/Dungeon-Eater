using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//仅存储敌人信息,复杂的索引动态地分析,不再存储
//[CreateAssetMenu(menuName = "DataBase/CardDatabase")]
public class EnemyDataBase : DataBase<string, EnemyData>
{
    // --------------------- 单例访问 ---------------------
    // 这块逻辑提取成基类需要泛型,Resources.Load使用泛型会有问题
    private static EnemyDataBase _instance;
    public static EnemyDataBase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<EnemyDataBase>("EnemyDatabase");
                if (_instance == null)
                    Debug.LogError("EnemyDatabase.asset not found in Resources folder!");
                else
                    _instance.Init();
            }
            return _instance;
        }
    }

    // --------------------- 查询接口 ---------------------

    /// <summary> 通过卡牌ID查找模板 </summary>
    public static EnemyData GetById(string id)
    {
        return Instance.idLookup.TryGetValue(id, out var e)
            ? e
            : null;
    }


    /// <summary> 获取所有敌人（只读） </summary>
    public static IReadOnlyList<EnemyData> AllEnemies => Instance.datas;
}
