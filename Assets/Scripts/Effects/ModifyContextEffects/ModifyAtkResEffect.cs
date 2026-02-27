using System.Collections.Generic;

public class ModifyAtkResEffect : Effect
{
    public Result overrideResult;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster, EffectContext context)
    {
        return new ModifyAttackResGA(overrideResult);
    }
}
