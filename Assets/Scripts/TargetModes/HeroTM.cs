using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 返回英雄对象
/// </summary>
public class HeroTM : TargetMode
{
    public override List<CombatantView> GetTargets(CombatantView manualTarget)
    {
        List<CombatantView> targets = new()
        {
            HeroSystem.Instance.HeroView
        };
        return targets;
    }
}
