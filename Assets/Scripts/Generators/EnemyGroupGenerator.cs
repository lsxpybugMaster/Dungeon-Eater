using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    调用时决定生成的敌人
 */
public class EnemyGroupGenerator 
{
    private System.Random rng;
    
    public EnemyGroupGenerator()
    {
        rng = GameManager.Instance.RogueController.GetStream("Enemy");
    }

    /// <summary>
    /// 依据关卡和随机数生成器生成敌人
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
            //int n = EnemyGroupDatabase.GetRandomGroupByLevel(difficulty);
        }
        else
        {
            //return -;
        }
        return default;
    }
}
