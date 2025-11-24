using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageGA : GameAction, IHaveCaster
{
    public int Amount {  get; set; }
    //存储攻击指向的对象
    public List<CombatantView> Targets { get; set; }

    public CombatantView Caster { get; private set; }

    //在执行DealDamage前需要PreReaction判定,如果判定失败则停止该反应
    public bool ShouldCancel { get; set; } = false; 

    public DealDamageGA(int amount, List<CombatantView> targets, CombatantView caster)
    {
        Amount = amount;
        Targets = new(targets);
        Caster = caster;
    }
}
