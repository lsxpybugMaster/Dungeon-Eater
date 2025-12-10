using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction
{
    //在ActionSystem的Flow中按顺序解析并执行

    //执行动作之前反应
    public List<GameAction> PreReactions { get; private set; } = new();

    //执行动作时反应
    public List<GameAction> PerformReactions { get; private set; } = new();

    //执行动作后反应
    public List<GameAction> PostReactions { get; private set;} = new();
}


