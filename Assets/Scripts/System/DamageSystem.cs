using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 战斗数值相关系统
/// </summary>
public class DamageSystem : MonoBehaviour
{
    [SerializeField] private GameObject damageVFX;

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
        foreach (var target in targets)
        {
            //STEP: 造成伤害
            target.M.Damage(damage);

            //STEP: 播放特效
            Instantiate(damageVFX, target.transform.position, Quaternion.identity);
            //时间一定要用黑板变量统一控制
            yield return new WaitForSeconds(Config.Instance.effectTime);

            //STEP: 判断该伤害是否"致命"
            CheckTargetHealthState(target);
        }
    }

    private void CheckTargetHealthState(CombatantView target)
    {
        if (target.M.CurrentHealth <= 0)
        {
            //enemyView即Enemy类
            if (target is EnemyView enemyView)
            {
                KillEnemyGA killEnemyGA = new(enemyView);
                ActionSystem.Instance.AddReaction(killEnemyGA);
            }
            else if (target is HeroView heroView)
            {
                Debug.Log("玩家寄了");

                PlayerFailGA playerFailGA = new(heroView);
                ActionSystem.Instance.AddReaction(playerFailGA);
                //Game Over
            }
        }
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
      

        Result res;
        switch (res = CheckUtil.AttackRoll(ga.Caster, ga.Targets[0], attackThrowBuff))
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
