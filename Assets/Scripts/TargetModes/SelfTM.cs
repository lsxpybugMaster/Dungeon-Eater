using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//仅作为标记,内部逻辑翻译并解析
public class SelfTM : TargetMode
{
    public override List<CombatantView> GetTargets(CombatantView manualTarget)
    {
        return new(){ manualTarget };
    }
}
