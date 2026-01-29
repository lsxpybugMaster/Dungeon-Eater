using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Obsolete("可以用添加属性效果代替")]

//强化对应属性: 
public class BoostMultiAtkIntend : EnemyIntend
{
    //每次增加的数目
    public int add = 1;
    public override GameAction GetGameAction(EnemyView enemy)
    {
        BoostMultiAttackGA boostMultiAttackGA = new(enemy, add);
        return boostMultiAttackGA;
    }
}
