using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIntend : EnemyIntend
{
    //伤害由Enemy.AttackPower决定
    public override GameAction GetGameAction(EnemyView enemy)
    {
        //DISCUSS: 还是留着敌人进攻GA,用于标识这是来自敌人的攻击
        AttackHeroGA attackHeroGA = new(enemy);
        return attackHeroGA;
    }
}
