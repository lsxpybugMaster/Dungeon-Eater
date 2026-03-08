using SerializeReferenceEditor;
using UnityEngine;

public class CondAttackIntend : EnemyIntend, IHaveDmgInfo
{
    public string dmgBase;
    public string dmgBuff; //根据条件增加伤害

    [SerializeReference, SR] private TargetMode conditionTarget;
    public StatusEffectType statusEffectCond;

    //FIXME: 这样写如果玩家更新了状态, 会导致Intend不对, 所以需要一个动态解析功能
    public string GetDmgInfo(EnemyView enemyView)
    {
        CombatantView view = conditionTarget.GetType() == typeof(HeroTM) ?
                             HeroSystem.Instance.HeroView :
                             enemyView;

        if (view.M.GetStatusEffectStacks(statusEffectCond) > 0)
            return dmgBase + "+" + dmgBuff;
        else
            return dmgBase;
    }

    public override GameAction GetGameAction(EnemyView enemy)
    {
        //
        AttackHeroGA ga = new(enemy, EnemySkill.Attack, attackDmgDice:GetDmgInfo(enemy));
        return ga;
    }
}
