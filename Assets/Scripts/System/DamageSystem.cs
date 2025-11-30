using ActionSystemTest;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

        ActionSystem.AttachPerformer<DealFixedAttackGA>(FixedAttackPerformer);
        ActionSystem.SubscribeReaction<DealFixedAttackGA>(DealBeforeFixedAttack, ReactionTiming.PRE);
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


    //处理攻击掷骰(为处理伤害事件的预先反应)
    private void DealBeforeFixedAttack(DealFixedAttackGA ga)
    {
        BattleInfoUI.Instance.AddFixedResult(ga.FixedDamage, ga.Caster);
    }


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

    //处理固定伤害的事件
    private IEnumerator FixedAttackPerformer(DealFixedAttackGA ga)
    {
        yield return AttackAnim(ga.Caster, new Vector2(0.5f, 0), Config.Instance.attackTime);
        yield return DealDamage(ga.Targets, ga.FixedDamage);
    }

    public IEnumerator DealDamage(List<CombatantView> targets, int damage)
    {
        foreach (var target in targets)
        {
            target.M.Damage(damage);

            Instantiate(damageVFX, target.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.15f);

            if (target.M.CurrentHealth <= 0)
            {
                //enemyView即Enemy类
                if (target is EnemyView enemyView)
                {
                    KillEnemyGA killEnemyGA = new(enemyView);
                    ActionSystem.Instance.AddReaction(killEnemyGA);
                }
                else
                {
                    //Game Over
                }
            }
        }
    }

    /// <summary>
    /// 处理全套攻击逻辑(攻击判定 + 数值判定)
    /// </summary>
    private IEnumerator DealAttackPerformer(DealAttackGA ga)
    {
        //攻击掷骰判定
        //TODO: 从攻击者和受击者中提取攻击掷骰修正值
        int attackDice = DiceRollUtil.D20();
        
        //决定事件
        // MISS
        if (attackDice < 10)
        {
            BattleInfoUI.Instance.AddFailedResult(attackDice, 10, "20", ga.Caster);

            ActionSystem.Instance.AddReaction(new MissGA(ga.Targets, ga.Caster));

            yield break;
        }

        // HIT or Critcal HIT
        int damageDice = DiceRollUtil.DfromString(ga.DiceStr);

        if (attackDice >= 12) 
        {
            BattleInfoUI.Instance.AddGaintSuccessResult(attackDice, ga.Caster);

            BattleInfoUI.Instance.AddThrowResult(2 * damageDice, ga.DiceStr);

            ActionSystem.Instance.AddReaction(new CriticalHitGA(attackDice * 2, ga.Targets, ga.Caster));
        }
        else
        {
            BattleInfoUI.Instance.AddSuccessResult(attackDice, 10, "20", ga.Caster);

            BattleInfoUI.Instance.AddThrowResult(damageDice, ga.DiceStr);

            ActionSystem.Instance.AddReaction(new NormalAttackGA(damageDice, ga.Targets, ga.Caster));
        }
       
    }
}
