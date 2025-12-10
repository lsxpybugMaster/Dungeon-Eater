using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 与选择的主目标相同,没有Manual时不允许使用!!!
/// </summary>
public class SameAsManualTM : TargetMode
{
    //这个函数不返回,因为该类只作为一个标签类
    public override List<CombatantView> GetTargets()
    {
        return null;
    }
}
