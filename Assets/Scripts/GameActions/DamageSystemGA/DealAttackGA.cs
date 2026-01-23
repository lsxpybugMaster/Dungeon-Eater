using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealAttackGA : GameAction, IHaveCaster, INeedContext, IBuffable
{
    //掷骰信息: 如1d6
    public string DiceStr => DiceStr_Base + DiceStr_Buff;
    public string DiceStr_Buff { get; set; }
    public string DiceStr_Base { get; private set; }

    //由子类计算
    public int Damage { get; set; } 
    private string Damage_Buff;

    //存储攻击指向的对象
    public List<CombatantView> Targets { get; set; }

    public CombatantView Caster { get; private set; }

    //记录上下文
    public EffectContext Context { get; private set; }

    public DealAttackGA(string diceStr, List<CombatantView> targets, CombatantView caster, EffectContext context)
    {
        DiceStr_Base = diceStr;
        Targets = targets;
        Caster = caster;
        Context = context;
    }
}
