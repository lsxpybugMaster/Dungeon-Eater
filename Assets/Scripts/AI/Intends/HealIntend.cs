using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: 目前不支持目标选择
public class HealIntend : EnemyIntend
{
    [SerializeField] private int healAmount;
    public override GameAction GetGameAction(EnemyView enemy)
    {
        HealGA healGA = new(healAmount, new() { enemy }, enemy);
        return healGA;
    }
}
