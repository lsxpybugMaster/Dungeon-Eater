using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 占位Intend,用于模拟跳过敌人回合的效果
/// </summary>
public class NoIntend : EnemyIntend
{
    public override GameAction GetGameAction(EnemyView enemy)
    {
        return new EmptyGameAction();
    }
}
