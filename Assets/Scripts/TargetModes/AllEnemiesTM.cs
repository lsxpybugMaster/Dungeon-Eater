using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemiesTM : TargetMode
{
    public override List<CombatantView> GetTargets()
    {
        //直接返回全部敌人列表
        return new(EnemySystem.Instance.Enemies);
    }
}
