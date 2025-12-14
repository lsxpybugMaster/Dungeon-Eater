using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 减少效果层数,可用于外部Effect配置等
/// </summary>
public class DecayEffectGA : GameAction
{
    //减少多少层效果
    public int Decay { get; private set; }

    public StatusEffectType Type { get; private set; }
    public CombatantView Target { get; private set; }

    public DecayEffectGA(int decay, StatusEffectType type, CombatantView target)
    {
        Decay = decay;
        Target = target;
        Type = type;
    }
}
