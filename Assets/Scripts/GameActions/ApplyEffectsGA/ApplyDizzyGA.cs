using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//模拟被眩晕类效果控制,目前是什么也不干
public class ApplyDizzyGA : GameAction
{
    public CombatantView Target { get; private set; }

    public ApplyDizzyGA(CombatantView target)
    {
        Target = target;
    }
}
