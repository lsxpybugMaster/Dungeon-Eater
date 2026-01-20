using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//参考AddStatusEffectEffect
public class AddStatusEffectIntend : EnemyIntend
{
    [SerializeField] private StatusEffectType statusEffectType;

    [SerializeField] private int stackCount;

    public override GameAction GetGameAction(EnemyView enemy)
    {
        // new(){enemy} 即 new List<CombantantView>(){enemy}
        return new AddStatusEffectGA(statusEffectType, stackCount, new(){enemy});
    }
}