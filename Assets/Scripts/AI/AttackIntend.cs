using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIntend : EnemyIntend
{
    //伤害由Enemy.AttackPower决定
    public override GameAction GetGameAction(EnemyView enemy)
    {
        AttackHeroGA attackHeroGA = new(enemy);
        return attackHeroGA;
    }
}
