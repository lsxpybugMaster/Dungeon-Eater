using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] private GameObject damageVFX;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
    }

    /// <summary>
    /// 处理伤害的核心事件执行器
    /// </summary>
    /// <param name="dealDamageGA"></param>
    /// <returns></returns>
    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        var target = dealDamageGA.DamageTaker;
        int damage = dealDamageGA.Damage;

        //STEP: 造成伤害
        target.M.Damage(damage);

        //STEP: 播放特效
        Instantiate(damageVFX, target.transform.position, Quaternion.identity);
        //时间一定要用黑板变量统一控制
        yield return new WaitForSeconds(Config.Instance.effectTime);

        //STEP: 判断该伤害是否"致命"
        CheckTargetHealthState(target);
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

}
