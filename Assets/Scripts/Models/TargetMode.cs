using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class TargetMode
{
    //抽象方法,返回值为所有触发动作的目标
    public abstract List<CombatantView> GetTargets();
}
