using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 与DealAttackGA不同,仅允许直接造成伤害,不进行攻击掷骰等其他判定
/// </summary>
public class DealFixedAttackGA : GameAction, IHaveCaster
{
    public int FixedDamage {  get; set; }
    //存储攻击指向的对象
    public List<CombatantView> Targets { get; set; }

    public CombatantView Caster { get; private set; }

    //在执行DealDamage前需要PreReaction判定,如果判定失败则停止该反应
    public bool ShouldCancel { get; set; } = false; 

    public DealFixedAttackGA(int amount, List<CombatantView> targets, CombatantView caster)
    {
        FixedDamage = amount;
        Targets = new(targets);
        Caster = caster;
    }
}
