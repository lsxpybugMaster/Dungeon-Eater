using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    动态存储当前关卡下的敌人
    解析EnemyDataBase并使用字典封装
    EnemiesGroup => EnemyGroup => enemies (List<EnemyData>)
 */
public class EnemyPool
{
    // 存储难度分 => 敌人
    public Dictionary<int, List<EnemyData>> EnemiesDifficulty { get; private set; }

    // 存储敌人小组
    public List<EnemyGroup> EnemiesGroup { get; private set; }
    public List<EnemyGroup> BossGroup { get; private set; }

    // 存储敌人缓存,用于下一次生成的敌人,可以直接读写
    public List<EnemyData> EnemiesBuffer { get; set; }

    // 后续添加 => 生成权重
    //public Dictionary<int, float> EnemiesChosenWeight;

    // 根据条件初始化
    public EnemyPool(int level)
    {
        EnemiesDifficulty = new();
        EnemiesBuffer = new();
        // 动态解析一次EnemyDataBase
        foreach (EnemyData enemyData in EnemyDataBase.AllEnemies)
        {
            if (!enemyData.AppearLevels.Contains(level))
                continue;

            int diff = enemyData.Diffculty;
            if (!EnemiesDifficulty.TryGetValue(diff, out var list))
            {
                list = new List<EnemyData>();
                EnemiesDifficulty.Add(diff, list);
            }

            list.Add(enemyData);
        }

        // 动态解析一次EnemyGroup
        EnemiesGroup = new(EnemyGroupDatabase.GetGroupsByLevel(level).Groups);
        BossGroup = new(BossGroupDataBase.GetBossGroupsByLevel(level).Groups);

        DebugUtil.Yellow($"关卡{level}敌人数据准备完成： {EnemiesDifficulty[1].Count}个");
        DebugUtil.Yellow($"关卡{level}BOSS数据准备完成： {BossGroup.Count}个");
        DebugUtil.Yellow($"关卡{level}敌人组合数据准备完成： {EnemiesGroup.Count}个");
    }

    public List<EnemyData> GetEnemiesBuffer()
    {
        return EnemiesBuffer;
    }

    public void SetEnemiesBuffer(List<EnemyData> enemiesBuffer)
    {
        EnemiesBuffer = enemiesBuffer;
    }

    public EnemyData GetEnemyByDifficulty(int diff)
    {
        if (EnemiesDifficulty.Count == 0)
        {
            Debug.LogWarning($"难度系数 {diff} 下未检索到敌人");
        }
        return EnemiesDifficulty[diff].GetRandom();
    }

    public EnemyData GetEnemyByDifficulty(int diff, System.Random rng)
    {
        if (EnemiesDifficulty.Count == 0)
        {
            Debug.LogWarning($"难度系数 {diff} 下未检索到敌人");
        }
        return EnemiesDifficulty[diff].GetRandom(rng);
    }

    /// <summary>
    /// 获取XXXGroup的随机一行敌人,rng为空时GetRandom会自动识别并随机
    /// </summary>
    /// <param name="type"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    public List<EnemyData> GetRandomEnemyGroup(BattleType type, System.Random rng = null)
    {
        List<EnemyGroup> tarGroup = type switch
        {
            BattleType.Normal => EnemiesGroup,
            BattleType.Elite => default,
            BattleType.Boss => BossGroup,
            _ => null
        };

        if (tarGroup == null || tarGroup.Count == 0)
        {
            Debug.LogError($"[{type}] 下的 tarGroup 不合法");
            return null;
        }

        return tarGroup.GetRandom(rng).Enemies;
    }
}
