using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    调用时决定生成的敌人
    创建对象时绑定对应的敌人池
    敌人池由Gamemanger初始化

 */
/// <summary>
/// 由 <see cref="MapGenerator"> 负责绑定敌人
/// </summary>
public class EnemyGroupGenerator 
{
    // private System.Random rng;
    // private System.Random bossRng;
    private EnemyPool enemyPool;
    
    public EnemyGroupGenerator()
    {
        //rng = GameManager.Instance.RogueController.GetStream("Enemy");
        //bossRng = GameManager.Instance.RogueController.GetStream("Boss");
        enemyPool = GameManager.Instance.EnemyPool;
    }

    //新版敌人生成逻辑
    //其实就是现在房间不再生成静态敌人，而是静态随机数
    //通过将其包装成随机数生成器 + 动态diff生成敌人
    public List<EnemyData> GenerateEnemies(MapGrid grid, int difficulty)
    {
        //房间单独的随机生成器, 确保房间可控 + 动态生成敌人
        var room_rng = new System.Random(grid.enemySeed);

        //33%概率生成固定敌人
        int fixedEnemy = room_rng.Next(0, 3);
        if (fixedEnemy == 0)
        {
            return enemyPool.GetEnemyGroupByDiff(difficulty);
            //return enemyPool.GetRandomEnemyGroup(BattleType.Normal, room_rng);
        }
        //否则生成随机敌人组合
        else
        {
            return CalculateEnemyGroup(difficulty, room_rng);
        }
    }

    public List<EnemyData> GeneraterBoss(MapGrid grid)
    {
        //房间单独的随机生成器, 确保房间可控 + 动态生成敌人
        var room_rng = new System.Random(grid.enemySeed);

        return enemyPool.GetRandomEnemyGroup(BattleType.Boss, room_rng);
    }


    private List<EnemyData> CalculateEnemyGroup(int diff, System.Random rng)
    {
        List<EnemyData> group = new();
        
        while(group.Count < 3 && diff > 0)
        {
            //难度目前只有1, 2, 3
            int enemyDiff = Mathf.Min(diff, rng.Next(1, 4));
                   
            group.Add(enemyPool.GetEnemyByDifficulty(enemyDiff, rng));

            diff -= enemyDiff;        
        }

        return group;
    }


    ///// <summary>
    ///// 依据关卡和随机数生成器生成普通敌人
    ///// </summary>
    ///// <param name="level"></param>
    ///// <param name="difficulty"></param>
    ///// <returns></returns>
    //public List<EnemyData> GetEnemyGroup(int difficulty)
    //{
    //    //是否生成固定序列
    //    int fixflag = rng.Next(0, 2);
    //    if (fixflag == 1)
    //    {
    //        return enemyPool.GetRandomEnemyGroup(BattleType.Normal , rng);
    //    }
    //    //否则根据难度分数挑选敌人
    //    else
    //    {
    //        return CalculateEnemyGroup(difficulty, rng);
    //    }
    //}
}
