using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 判断敌人生命是否小于
/// </summary>
public class HPCondition : IntendCondition
{
    [Range(0f, 1f)]
    public float threshold;


    public override bool Evaluate(EnemyView enemy)
    {
        return enemy.M.CurrentHealth / (float)enemy.M.MaxHealth < threshold;
    }
}
