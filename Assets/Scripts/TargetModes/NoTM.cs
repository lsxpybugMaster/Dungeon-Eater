using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTM : TargetMode
{
    public override List<CombatantView> GetTargets(CombatantView manualTarget)
    {
        return null;
    }
}
