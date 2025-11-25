using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealAttackGA : GameAction, IHaveCaster
{
    //掷骰信息: 如1d6
    public string DiceStr { get; set; }

    //由子类计算
    public int Damage { get; set; } 

    //存储攻击指向的对象
    public List<CombatantView> Targets { get; set; }

    public CombatantView Caster { get; private set; }


    public DealAttackGA(string diceStr, List<CombatantView> targets, CombatantView caster)
    {
        DiceStr = diceStr;
        Targets = targets;
        Caster = caster;
    }
}
