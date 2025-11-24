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
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
        ActionSystem.SubscribeReaction<DealDamageGA>(DealAttackRoll, ReactionTiming.PRE);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
        ActionSystem.UnsubscribeReaction<DealDamageGA>(DealAttackRoll, ReactionTiming.PRE);
    }

    //处理攻击掷骰(为处理伤害事件的预先反应)
    private void DealAttackRoll(DealDamageGA dealDamageGA)
    {
        int d20 = DiceRollUtil.D20();
        if (d20 < 10)
        {
            dealDamageGA.ShouldCancel = true;
            BattleInfoUI.Instance.AddFailedResult(d20, 10, "20");
        }
        else BattleInfoUI.Instance.AddThrowResult(d20, "20");
    }


    //处理伤害的事件
    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        if (dealDamageGA.ShouldCancel)
        {
            Debug.LogWarning("造成伤害的事件被强行停止了!");
            yield break;
        }

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
}
