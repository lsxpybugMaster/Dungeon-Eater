using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 永远返回真
/// </summary>
public class NoCondition : IntendCondition
{
    public override bool Evaluate(EnemyView enemy)
    {
        return true;
    }
}
