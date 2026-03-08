using System.Collections;

/*
    AttackModifySystem 决定战斗伤害细节 ==> 由DamageSystem掷骰处理 
 */
public class AttackModifySystem : IActionPerformerSystem
{
    public void Register()
    {
        ActionSystem.AttachPerformer<MagnifyAttackGA>(MagnifyAttackPerformer);
        ActionSystem.AttachPerformer<ModifyAttackResGA>(ModifyAttackResPerformer);
    }

    public void UnRegister()
    {
        ActionSystem.DetachPerformer<MagnifyAttackGA>();
        ActionSystem.DetachPerformer<ModifyAttackResGA>();
    }

    private IEnumerator MagnifyAttackPerformer(MagnifyAttackGA ga)
    {
        //解析攻击倍率(初始时为0)
        //int muti = ga.Caster.M.GetStatusEffectStacks(StatusEffectType.MUTIATK) + 1;

        //组合攻击倍率字符串 nd[x] x为ga中数据
        //string dmg = muti.ToString() + "d" + ga.damageOnce.ToString();

        //攻击字符串应当直接保留在ga中而非解析!!
        string dmg = ga.DmgStr; 

        //最后都是变成 dealAttackGA
        DealAttackGA dealAttackGA = new(dmg, ga.Target, ga.Caster, null);
        ActionSystem.Instance.AddReaction(dealAttackGA);

        yield return null;
    }

    private IEnumerator ModifyAttackResPerformer(ModifyAttackResGA ga)
    {
        Result overrideRes = ga.OverrideResult;
        //直接修改上下文(不安全)

        BattleAttackSystem.Instance.ModifierContext.ModifyResult(overrideRes);
    
        yield return null;
    }

}
