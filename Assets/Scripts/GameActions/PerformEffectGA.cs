using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ö���ʱ,��Ҫָ����ִ�е�Ч��effect
/// </summary>
public class PerformEffectGA : GameAction
{
    public Effect Effect { get; set; }

    public PerformEffectGA(Effect effect)
    {
        Effect = effect;
    }
}
