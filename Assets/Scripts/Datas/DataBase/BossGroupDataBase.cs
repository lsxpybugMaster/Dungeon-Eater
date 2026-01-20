using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DataBase只允许封装一个字典,Boss数据较为独特,不得不加新的一个Base4
/// TODO: 是否能够优化上面的问题?
/// </summary>
[CreateAssetMenu(menuName = "DataBase/BossGroupDataBase")]
public class BossGroupDataBase : DataBase<int, EnemyGroupsData>
{
    public static IReadOnlyList<EnemyGroupsData> AllGroups => Instance.datas;

    // --------------------- 单例访问 ---------------------
    // 这块逻辑提取成基类需要泛型,Resources.Load使用泛型会有问题
    private static BossGroupDataBase _instance;
    public static BossGroupDataBase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<BossGroupDataBase>(nameof(BossGroupDataBase));
                if (_instance == null)
                    Debug.LogError("EnemyGroupDatabase.asset not found in Resources folder!");
                else
                    _instance.Init();
            }
            return _instance;
        }
    }

    public static EnemyGroupsData GetBossGroupsByLevel(int level)
    {
        return Instance.idLookup[level];
    }


    public static EnemyGroup GetRandomBossGroupByLevel(int id, System.Random rng)
    {
        EnemyGroupsData data = Instance.idLookup[id];
        return data.Groups.GetRandom(rng);
    }
}
