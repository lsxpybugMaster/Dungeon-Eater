using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//增加属性: Context.MultiAtk
public class BoostMultiAttackGA : GameAction
{
    public int add {  get; private set; } 
    public CombatantView Caster { get; set; } //后面可能会根据强化者的不同做出不同的反应

    public BoostMultiAttackGA(CombatantView caster, int add)
    {
        Caster = caster;
        this.add = add;
    }
}
