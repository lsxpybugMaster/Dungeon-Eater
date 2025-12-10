using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 与选择的主目标相同,没有Manual时不允许使用!!!
/// </summary>
public class SameAsManualTM : TargetMode
{
    /// <summary>
    /// 这里利用override,使用多态完成了TargetMode的动态配置
    /// </summary>
    /// <param name="manualTarget"></param>
    /// <returns></returns>
    public override List<CombatantView> GetTargets(CombatantView manualTarget)
    {
        //该函数需要利用 manualTarget, override后就对其进行分析, 其他子类不管即可
        return new() { manualTarget };
    }
}
