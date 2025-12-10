using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 专门用于支持多GA依序执行的功能
/// 原始是AS由于有isPerforming,同时在代码中声明多个GA可能导致冲突
/// </summary>
public class PerformAllGA : GameAction
{
    private readonly List<GameAction> sequence = new();

    //params 关键字允许方法接受可变数量的参数，这些参数会被自动封装为一个数组。
    public PerformAllGA(params GameAction[] actions)
    {
        sequence.AddRange(actions); //相当于vector.insert(vector.end(), vec.begin(), vec.end())
    }

    public void Add(GameAction ga)
    {
        sequence.Add(ga);
    }

    public List<GameAction> GetSequence() => sequence;
}

//用于占位的GA
public class EmptyGA : GameAction
{
    
}
