using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//参考AddStatusEffectEffect
public class AddStatusEffectIntend : EnemyIntend
{
    [SerializeField] private StatusEffectType statusEffectType;

    [SerializeField] private int stackCount;

    [field: SerializeReference, SR] private TargetMode targetMode = new NoTM();

    public override GameAction GetGameAction(EnemyView enemy)
    {
        // new(){enemy} 即 new List<CombantantView>(){enemy}
        List<CombatantView> targets = new();
        if (targetMode == null || targetMode is SelfTM)
            targets.Add(enemy);
        else
            targets = targetMode.GetTargets(null);
            
        return new AddStatusEffectGA(statusEffectType, stackCount, targets);
    }
}

public class AddRandomStatusEffectIntend : EnemyIntend
{
    [SerializeField] private StatusEffectType statusEffectType;

    [SerializeField] private string stackCountDice;

    [field: SerializeReference, SR] private TargetMode targetMode;

    public override GameAction GetGameAction(EnemyView enemy)
    {
        //保证target不为null
        List<CombatantView> targets = TargetMode.GetNotNullTargets(enemy, targetMode);

        return new AddRandomStatusEffectGA(statusEffectType, stackCountDice, targets);
    }
}
