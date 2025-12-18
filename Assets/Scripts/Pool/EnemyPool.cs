using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    动态存储当前关卡下的敌人
    解析EnemyDataBase并使用字典封装
 */
public class EnemyPool
{
    //存储难度分 => 敌人
    public Dictionary<int, List<EnemyData>> EnemiesDifficulty {  get; private set; }
    //后续添加 => 生成权重
    //public Dictionary<int, float> EnemiesChosenWeight;

    //根据条件初始化
    public EnemyPool(int level)
    {
        EnemiesDifficulty = new();
        //动态解析一次EnemyDataBase
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

        DebugUtil.Yellow($"关卡{level}敌人数据准备完成： {EnemiesDifficulty[1].Count }个");
    }

    public EnemyData GetEnemyByDifficulty(int diff)
    {
        return EnemiesDifficulty[diff].GetRamdom();
    }
}
