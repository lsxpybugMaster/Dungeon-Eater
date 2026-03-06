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

    //FIXME: 架构问题导致需要这个辅助函数
    //静态工具函数, 保证不会返回null,以免造成一些API的空指针异常
    public static List<CombatantView> GetNotNullTargets(CombatantView manualTarget, TargetMode targetMode)
    {
        List<CombatantView> targets = new();
        if (targetMode == null || targetMode is SelfTM)
            targets.Add(manualTarget); //保证不会出现null
        else
            targets = targetMode.GetTargets(null);

        return targets;
    }
}
