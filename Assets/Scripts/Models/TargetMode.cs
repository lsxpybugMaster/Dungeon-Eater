using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class TargetMode
{
    /// <summary>
    /// 抽象方法,返回值为所有触发动作的目标
    /// </summary>
    /// <param name="manualTarget">如果无需该函数,传入空参</param>
    /// <returns></returns>
    public abstract List<CombatantView> GetTargets(CombatantView manualTarget);
}
