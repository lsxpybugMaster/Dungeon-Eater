using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 由Combantant.multi 控制攻击倍率 造成 n * 数值的伤害
/// </summary>
public class MagnifyAttackGA : GameAction
{
    public int damageOnce { get; private set; }

    public CombatantView Attacker { get; private set; }
    public CombatantView Caster { get; private set; }


    public MagnifyAttackGA(CombatantView attacker, CombatantView caster, int dmg)
    {
        Attacker = attacker;
        Caster = caster;
        damageOnce = dmg;
    }
}
