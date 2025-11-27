using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackIntend : EnemyIntend
{
    [SerializeField] private EnemySkill skillType;
    //伤害由Enemy.AttackPower决定 ==> 根据具体类型分发攻击事件 
    public override GameAction GetGameAction(EnemyView enemy)
    {
        //DISCUSS: 还是留着敌人进攻GA,用于标识这是来自敌人的攻击
        AttackHeroGA attackHeroGA = new(enemy, skillType);
        return attackHeroGA;
    }
}
