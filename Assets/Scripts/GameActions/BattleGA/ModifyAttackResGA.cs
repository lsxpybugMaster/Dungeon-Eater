using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通过对全局Context修改(不安全)来变相实现Action Modifier
/// 最终还是需要重构为基于Action的系统!!
/// <see cref="AttackModifySystem">
/// </summary>
public class ModifyAttackResGA : GameAction
{
    public Result OverrideResult { get; private set; }

    public ModifyAttackResGA(Result overrideRes)
    {
        OverrideResult = overrideRes;
    }
}
