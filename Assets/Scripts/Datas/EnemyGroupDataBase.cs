using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/EnemyGroupDatabase")]
public class EnemyGroupDatabase : DataBase<int, EnemyGroupData>
{
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

    /// <summary> 通过Level ID获取随机序列 </summary>
    public static EnemyGroup GetRandomGroupByLevel(int id)
    {
        EnemyGroupData data = Instance.idLookup[id];
        return data.Groups.GetRamdom();
    }
}
