using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/EnemyGroupDatabase")]
public class EnemyGroupDatabase : DataBase<int, EnemyGroupsData>
{
    public static IReadOnlyList<EnemyGroupsData> AllGroups => Instance.datas;

    // --------------------- 单例访问 ---------------------
    // 这块逻辑提取成基类需要泛型,Resources.Load使用泛型会有问题
    private static EnemyGroupDatabase _instance;
    public static EnemyGroupDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<EnemyGroupDatabase>(nameof(EnemyGroupDatabase));
                if (_instance == null)
                    Debug.LogError("EnemyGroupDatabase.asset not found in Resources folder!");
                else
                    _instance.Init();
            }
            return _instance;
        }
    }

    public static EnemyGroupsData GetGroupsByLevel(int level)
    {
        return Instance.idLookup[level];
    }

    /// <summary> 通过Level ID获取随机序列 </summary>
    public static EnemyGroup GetRandomGroupByLevel(int id)
    {
        EnemyGroupsData data = Instance.idLookup[id];
        return data.Groups.GetRandom();
    }

    public static EnemyGroup GetRandomGroupByLevel(int id, System.Random rng)
    {
        EnemyGroupsData data = Instance.idLookup[id];
        return data.Groups.GetRandom(rng);
    }

}
