using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗中改变战斗上下文的行动
/// </summary>
public class ChangeAIContextIntend : EnemyIntend
{
    public int contextUpdateNumber;
    public AI contextType;

    public override GameAction GetGameAction(EnemyView enemy)
    {
        UpdateAlContextGA updateAlContextGA = new(enemy, contextType, contextUpdateNumber);
        return updateAlContextGA;
    }
}
