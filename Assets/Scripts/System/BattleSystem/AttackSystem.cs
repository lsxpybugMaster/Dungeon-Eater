using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    AttackSystem决定战斗伤害细节 ==> 由DamageSystem掷骰处理 
 */
public class AttackSystem : MonoBehaviour
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<MagnifyAttackGA>(MagnifyAttackPerformer);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<MagnifyAttackGA>();
    }


    private IEnumerator MagnifyAttackPerformer(MagnifyAttackGA ga)
    {
        //解析攻击倍率
        int muti = ga.Caster.M.Contexts.MultiAtk;

        //组合攻击倍率字符串 nd[x] x为ga中数据
        string dmg = muti.ToString() + "d" + ga.damageOnce.ToString();

        //最后都是
        DealAttackGA dealAttackGA = new(dmg, new() { ga.Caster }, ga.Attacker, null);
        ActionSystem.Instance.AddReaction(dealAttackGA);

        yield return null;
    }
}
