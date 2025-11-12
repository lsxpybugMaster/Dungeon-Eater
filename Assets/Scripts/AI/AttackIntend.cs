using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIntend : EnemyIntend
{
    //独立于接口的外部参数手动配置
    [SerializeField] private int damage;
    public override GameAction GetGameAction(EnemyView enemy)
    {
        AttackHeroGA attackHeroGA = new(enemy);
        return attackHeroGA;
    }
}
