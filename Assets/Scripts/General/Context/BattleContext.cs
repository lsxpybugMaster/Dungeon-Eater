using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleType
{
    None, //空置,如果任何时间获取到该类别,说明出现问题(避免错误的使用了脏数据)
    Normal,
    Elite,
    Boss
}

//存储一些上下文信息用于Scene之间交互
public class BattleContext 
{
    public BattleType Type { get; private set; } = BattleType.None;

    public void Init(BattleType type)
    {
        if (Type != BattleType.None)
        {
            Debug.LogError("BattleContext 被重复初始化");
            return;
        }

        Type = type;
    }

    public void Invalidate()
    {
        Type = BattleType.None;
    }

}
