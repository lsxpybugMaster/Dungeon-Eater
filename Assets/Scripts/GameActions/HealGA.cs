using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: 生命回复的逻辑似乎与其他带回复值的逻辑高度重合？
public class HealGA : GameAction, IHaveCaster
{
    public int Amount { get; set; }
    public List<CombatantView> Targets { get; set; }
    public CombatantView Caster { get; private set; }

    public HealGA(int amount, List<CombatantView> targets, CombatantView caster)
    {
        Amount = amount;
        Targets = new(targets);
        Caster = caster;
    }
}
