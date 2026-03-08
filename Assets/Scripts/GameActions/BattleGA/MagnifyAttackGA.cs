using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 由Combantant.multi 控制攻击倍率 造成 n * 数值的伤害
/// </summary>
public class MagnifyAttackGA : GameAction
{ 
    public List<CombatantView> Target { get; private set; }
    public CombatantView Caster { get; private set; }


    public string DmgStr {  get; private set; }

    public MagnifyAttackGA(List<CombatantView> target, CombatantView caster, string dmg)
    {
        Target = target;
        Caster = caster;
        DmgStr = dmg;
    }
}
