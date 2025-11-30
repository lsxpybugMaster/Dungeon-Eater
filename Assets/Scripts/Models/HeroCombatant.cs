using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCombatant : Combatant
{
    //注意这里传入临时数据heroState
    public HeroCombatant(HeroState heroState)
    {
        CurrentHealth = heroState.CurrentHealth;
        MaxHealth = heroState.MaxHealth;
    }
}
