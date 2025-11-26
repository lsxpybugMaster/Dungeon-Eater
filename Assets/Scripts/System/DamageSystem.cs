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

        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
        ActionSystem.SubscribeReaction<DealDamageGA>(DealAttackRoll, ReactionTiming.PRE);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<DealAttackGA>();
        ActionSystem.DetachPerformer<NormalAttackGA>();
        ActionSystem.DetachPerformer<MissGA>();

        ActionSystem.DetachPerformer<DealDamageGA>();
        ActionSystem.UnsubscribeReaction<DealDamageGA>(DealAttackRoll, ReactionTiming.PRE);
    }

    //处理攻击掷骰(为处理伤害事件的预先反应)
    private void DealAttackRoll(DealDamageGA dealDamageGA)
    {
        //int d20 = DiceRollUtil.D20();
        //if (d20 < 10)
        //{
        //    dealDamageGA.ShouldCancel = true;

        //    //更新信息到文本
        //    BattleInfoUI.Instance.AddFailedResult(d20, 20, "20");

        //}
        //else BattleInfoUI.Instance.AddSuccessResult(d20, 20, "20");
    }

    private IEnumerator MissGAPerformer(MissGA ga)
    {
        var caster = ga.Caster;
        int drct = caster is HeroView ? 1 : -1;
        yield return MotionUtil.Dash(caster.transform, drct * new Vector2(0, 5), Config.Instance.attackTime);
    }


    private IEnumerator NormalAttackPerformer(NormalAttackGA ga)
    {
        var caster = ga.Caster;
        int drct = caster is HeroView ? 1 : -1;
        yield return MotionUtil.Dash(caster.transform, drct * new Vector2(1, 0), Config.Instance.attackTime);
        yield return DealDamage(ga);
    }

    public IEnumerator DealDamage(DealAttackGA dealAttackGA)
    {
        foreach (var target in dealAttackGA.Targets)
        {
            target.Damage(dealAttackGA.Damage);

            Instantiate(damageVFX, target.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.15f);

            if (target.CurrentHealth <= 0)
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

    //处理伤害的事件
    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        //if (dealDamageGA.ShouldCancel)
        //{
        //    Debug.LogWarning("造成伤害的事件被强行停止了!");
        //    yield break;
        //}

        foreach (var target in dealDamageGA.Targets)
        {
            target.Damage(dealDamageGA.Amount);

            Instantiate(damageVFX, target.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.15f);

            if (target.CurrentHealth <= 0)
            {
                //enemyView即Enemy类
                if(target is EnemyView enemyView)
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
        if (attackDice < 10)
        {
            BattleInfoUI.Instance.AddFailedResult(attackDice, 10, "20", ga.Caster);

            ActionSystem.Instance.AddReaction(new MissGA(ga.Targets, ga.Caster));

            yield break;
        }

        //添加检定结果到UI显示中
        BattleInfoUI.Instance.AddSuccessResult(attackDice, 10, "20", ga.Caster);
        int damageDice = DiceRollUtil.DfromString(ga.DiceStr);
        BattleInfoUI.Instance.AddThrowResult(damageDice, ga.DiceStr);

        ActionSystem.Instance.AddReaction(new NormalAttackGA(damageDice,ga.Targets,ga.Caster));
    }
}
