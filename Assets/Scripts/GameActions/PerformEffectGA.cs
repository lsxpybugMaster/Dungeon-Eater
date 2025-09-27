using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 创建该对象时,需要指定其执行的效果effect,以及该效果作用的目标
/// </summary>
public class PerformEffectGA : GameAction
{
    public Effect Effect { get; set; }

    public List<CombatantView> Targets { get; set; }

    public PerformEffectGA(Effect effect, List<CombatantView> targets)
    {
        Effect = effect;
        //防御性拷贝
        Targets = targets?.ToList();
    }
}
