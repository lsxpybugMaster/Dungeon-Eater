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
    public Dictionary<int, List<EnemyData>> EnemiesDifficulty {  get; private set; }

    // 存储敌人小组
    public List<EnemyGroup> EnemiesGroup { get; private set; } 

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
            
        DebugUtil.Yellow($"关卡{level}敌人数据准备完成： {EnemiesDifficulty[1].Count }个");
        DebugUtil.Yellow($"关卡{level}敌人组合数据准备完成： {EnemiesGroup.Count}个");
    }

    public List<EnemyData> GetEnemiesBuffer()
    {
        Debug.Log("GetEnemiesBuffer");
        return EnemiesBuffer;
    }

    public void SetEnemiesBuffer(List<EnemyData> enemiesBuffer)
    {
        Debug.Log("SetEnemiesBuffer");
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

    public List<EnemyData> GetEnemyListByDiffculty(int diff)
    {
        if (EnemiesGroup.Count == 0)
        {
            Debug.LogWarning($"难度系数 {diff} 下未检索到敌人组");
        }
        return EnemiesGroup.GetRandom().Enemies;
    }

    public List<EnemyData> GetEnemyListByDiffculty(int diff, System.Random rng)
    {
        if (EnemiesGroup.Count == 0)
        {
            Debug.LogWarning($"难度系数 {diff} 下未检索到敌人组");
        }
        return EnemiesGroup.GetRandom(rng).Enemies;
    }
}
