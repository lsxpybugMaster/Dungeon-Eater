using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatant : Combatant
{
    //添加更细粒度的攻击数据
    public int FixedAttackPower { get; set; }
    public string LightAttackPowerStr { get; set; }
    public string HeavyAttackPowerStr { get; set; }

    public EnemyCombatant(EnemyData enemyData)
    {
        CurrentHealth = enemyData.Health;

        MaxHealth = enemyData.Health;

        FixedAttackPower = enemyData.FixedAttack;

        LightAttackPowerStr = enemyData.LightAttackStr;

        HeavyAttackPowerStr = enemyData.HeavyAttackStr;

        Proficiency = enemyData.Proficiency;

        Flexbility = enemyData.Flexbility;
    }
}
