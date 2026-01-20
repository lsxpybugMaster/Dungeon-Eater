using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    调用时决定生成的敌人
    创建对象时绑定对应的敌人池
    敌人池由Gamemanger初始化
 */
public class EnemyGroupGenerator 
{
    private System.Random rng;
    private System.Random bossRng;
    private EnemyPool enemyPool;
    
    public EnemyGroupGenerator()
    {
        rng = GameManager.Instance.RogueController.GetStream("Enemy");
        bossRng = GameManager.Instance.RogueController.GetStream("Boss");
        enemyPool = GameManager.Instance.EnemyPool;
    }

    /// <summary>
    /// 依据关卡和随机数生成器生成普通敌人
    /// </summary>
    /// <param name="level"></param>
    /// <param name="difficulty"></param>
    /// <returns></returns>
    public List<EnemyData> GetEnemyGroup(int difficulty)
    {
        //是否生成固定序列
        int fixflag = rng.Next(0, 2);
        if (fixflag == 1)
        {
            return enemyPool.GetRandomEnemyGroup(BattleType.Normal , rng);
        }
        //否则根据难度分数挑选敌人
        else
        {
            return CalculateEnemyGroup(difficulty, rng);
        }
    }

    public List<EnemyData> GetBossGroup()
    {
        return enemyPool.GetRandomEnemyGroup(BattleType.Boss, bossRng);
    }

    private List<EnemyData> CalculateEnemyGroup(int diff, System.Random rng)
    {
        List<EnemyData> group = new();
        
        while(diff > 0)
        {
            //难度目前只有1, 2
            int enemyDiff = diff == 1? 1 : rng.Next(1, 3);
                   
            group.Add(enemyPool.GetEnemyByDifficulty(enemyDiff, rng));

            diff -= enemyDiff;        
        }

        return group;
    }
}
