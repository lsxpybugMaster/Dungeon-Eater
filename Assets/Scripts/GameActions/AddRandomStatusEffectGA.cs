using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRandomStatusEffectGA : AddStatusEffectGA
{
    //使用字符串并计数,由此计算出stackCount
    public string StackCountStr { get; private set; } 

    public AddRandomStatusEffectGA(StatusEffectType statusEffectType, string stackCountStr, List<CombatantView> targets, int stackCount = 0) : base(statusEffectType, stackCount, targets)
    {
        StackCountStr = stackCountStr;
    }

    public void SetStackCount(int value)
    {
        StackCount = value;
    }
}
