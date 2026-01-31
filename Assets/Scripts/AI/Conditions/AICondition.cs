using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 以AI枚举类为索引的条件判断
/// </summary>
public class AICondition : IntendCondition
{
    //全部在外部配置
    //条件
    public AI cond;
    //关系
    public Relation relation;
    //值
    public int x;


    public override bool Evaluate(EnemyView enemy)
    {
        AIContext aIContext = enemy.EnemyAI.aiContext;
        int currentValue = aIContext.Get(cond);
        return Compare[relation](currentValue, x);
    }
}
