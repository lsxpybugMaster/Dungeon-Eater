using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 创建该对象时,需要指定其执行的效果effect
/// </summary>
public class PerformEffectGA : GameAction
{
    public Effect Effect { get; set; }

    public PerformEffectGA(Effect effect)
    {
        Effect = effect;
    }
}
