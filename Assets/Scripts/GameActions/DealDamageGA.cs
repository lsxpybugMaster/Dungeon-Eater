using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageGA : GameAction
{
    public int Amount {  get; set; }
    //存储攻击指向的对象
    public List<CombatantView> Targets { get; set; }

    public DealDamageGA(int amount, List<CombatantView> targets)
    {
        Amount = amount;
        Targets = new(targets);
    }
}
