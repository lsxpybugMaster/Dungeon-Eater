using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
//NOTE: 更好的设计是创建一个GA修改系统
/// 为了简化设计,直接适用单例 + 全局上下文来管理
/// 可能会导致全局的污染问题, 需要及时Reset
/// </summary>
public class BattleModifierContext
{
    private Result modifyResult = Result.None;

    public void ModifyResult(Result newResult)
    {
        modifyResult = newResult;
    }

    /// <summary>
    /// 安全获取修改结果，获取后自动重置为None
    /// </summary>
    public Result ConsumeModifyResult()
    {
        var result = modifyResult;
        //[注意] 由于enum是值拷贝, 所以result不会被修改
        modifyResult = Result.None; // 获取后立即重置
        return result;
    }
}

/// <summary>
/// 战斗数值相关系统
/// </summary>
public class BattleAttackSystem : Singleton<BattleAttackSystem>
{
    public BattleModifierContext ModifierContext { get; private set; } = new();


    void OnEnable()
    {
        ActionSystem.AttachPerformer<DealAttackGA>(DealAttackPerformer);
        ActionSystem.AttachPerformer<NormalAttackGA>(NormalAttackPerformer);
        ActionSystem.AttachPerformer<MissGA>(MissGAPerformer);
        ActionSystem.AttachPerformer<CriticalHitGA>(CriticalHitPerformer);

        ActionSystem.AttachPerformer<DealFixedAttackGA>(DealFixedAttackPerformer);
        
        //通过注册反应来提前修改伤害相关GameAction
        ActionSystem.SubscribeReaction<DealFixedAttackGA>(DealBeforeFixedAttack, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<DealAttackGA>(DealBeforeAttack, ReactionTiming.PRE);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<DealAttackGA>();
        ActionSystem.DetachPerformer<NormalAttackGA>();
        ActionSystem.DetachPerformer<MissGA>();
        ActionSystem.DetachPerformer<CriticalHitGA>();

        ActionSystem.DetachPerformer<DealFixedAttackGA>();
        ActionSystem.UnsubscribeReaction<DealFixedAttackGA>(DealBeforeFixedAttack, ReactionTiming.PRE);
    }

    private IEnumerator AttackAnim(CombatantView caster, Vector2 direction, float attackTime)
    {
        int drct = caster is HeroView ? 1 : -1;
        yield return MotionUtil.Dash(caster.transform, drct * direction, attackTime);
    }


    //----------------------------------最终的处理部分-----------------------------------------
    private IEnumerator MissGAPerformer(MissGA ga)
    {
        yield return AttackAnim(ga.Caster, new Vector2(0,2.5f), Config.Instance.attackTime);
    }


    private IEnumerator NormalAttackPerformer(NormalAttackGA ga)
    {
        yield return AttackAnim(ga.Caster, new Vector2(1, 0), Config.Instance.attackTime);
        yield return DealDamage(ga.Targets, ga.Damage);
    }


    public IEnumerator CriticalHitPerformer(CriticalHitGA ga)
    {
        yield return AttackAnim(ga.Caster, new Vector2(3, 0), Config.Instance.attackTime);
        yield return DealDamage(ga.Targets, ga.Damage);
    }
    //---------------------------------------------------------------------------------------


    //处理攻击掷骰(为处理伤害事件的预先反应)
    private void DealBeforeFixedAttack(DealFixedAttackGA ga)
    {
        BattleInfoUI.Instance.AddFixedResult(ga.FixedDamage, ga.Caster);
    }

    //处理固定伤害的事件
    private IEnumerator DealFixedAttackPerformer(DealFixedAttackGA ga)
    {
        yield return AttackAnim(ga.Caster, new Vector2(0.5f, 0), Config.Instance.attackTime);
        yield return DealDamage(ga.Targets, ga.FixedDamage);
    }

    public IEnumerator DealDamage(List<CombatantView> targets, int damage)
    {
        //由 DamageSystem 中 DamageGA 处理真正的伤害
        foreach (var target in targets)
        {           
            ActionSystem.Instance.AddReaction(new DealDamageGA(target, damage));
        }    
        yield return null;
    }

    private void DealBeforeAttack(DealAttackGA dealAttackGA)
    {
        CombatantView caster = dealAttackGA.Caster;
        dealAttackGA.DiceStr_Buff = "";
        dealAttackGA.AttackThrowStr_Buff = "";
        if (caster.M.GetStatusEffectStacks(StatusEffectType.BLESS) > 0)
            dealAttackGA.DiceStr_Buff += "+1d4";

        if (caster.M.GetStatusEffectStacks(StatusEffectType.DRUNK) > 0)
        {
            dealAttackGA.DiceStr_Buff += "+1d10";
            dealAttackGA.AttackThrowStr_Buff += "-5";
        }

        if (caster.M.GetStatusEffectStacks(StatusEffectType.HEAVYHIT) > 0)
        {
            dealAttackGA.OverrideAttackResult = Result.GiantSuccess;
            caster.M.RemoveStatusEffect(StatusEffectType.HEAVYHIT, 1); //注意减1层
        }
    }


    /// <summary>
    //NOTE: 使用了事件分发
    /// 处理全套攻击逻辑(攻击判定 + 数值判定)
    /// </summary>
    private IEnumerator DealAttackPerformer(DealAttackGA ga)
    {
        //这里由于需要判定重击双倍伤害,所以没有直接使用封装好的工具类
        int damageDice = DiceRollUtil.DfromString(ga.DiceStr);

        //计算攻击掷骰的修正值
        int attackThrowBuff = DiceRollUtil.DfromString(ga.AttackThrowStr_Buff);

        //计算全局修正值
        //判断是否覆盖掷骰结果
        Result res;

        if (ga.OverrideAttackResult != Result.None)
        {
            res = ga.OverrideAttackResult;
            BattleInfoUI.Instance.AddInfo("[FixedDice!] Gaint Success",Color.cyan);
        }
        else
            res = CheckUtil.AttackRoll(ga.Caster, ga.Targets[0], attackThrowBuff);


        switch (res)
        {
            case Result.GiantSuccess:
                BattleInfoUI.Instance.AddThrowResult(2 * damageDice, ga.DiceStr);
                ActionSystem.Instance.AddReaction(new CriticalHitGA(damageDice * 2, ga.Targets, ga.Caster));
                break;

            case Result.Success:
                BattleInfoUI.Instance.AddThrowResult(damageDice, ga.DiceStr);
                ActionSystem.Instance.AddReaction(new NormalAttackGA(damageDice, ga.Targets, ga.Caster));
                break;

            case Result.Failure:
                ActionSystem.Instance.AddReaction(new MissGA(ga.Targets, ga.Caster));
                break;
        }

        ga.Context?.SetMainEffectSuccess(res);
        
        yield return null;
    }
}
