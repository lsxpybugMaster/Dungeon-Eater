using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

/*
    AttackSystem决定战斗伤害细节 ==> 由DamageSystem掷骰处理 
 */
public class AttackSystem : IActionPerformerSystem
{
    public void Register()
    {
        ActionSystem.AttachPerformer<MagnifyAttackGA>(MagnifyAttackPerformer);
    }

    public void UnRegister()
    {
        ActionSystem.DetachPerformer<MagnifyAttackGA>();
    }

    private IEnumerator MagnifyAttackPerformer(MagnifyAttackGA ga)
    {
        //解析攻击倍率(初始时为0)
        int muti = ga.Caster.M.GetStatusEffectStacks(StatusEffectType.MUTIATK) + 1;

        //组合攻击倍率字符串 nd[x] x为ga中数据
        string dmg = muti.ToString() + "d" + ga.damageOnce.ToString();

        //最后都是变成 dealAttackGA
        DealAttackGA dealAttackGA = new(dmg, ga.Target, ga.Caster, null);
        ActionSystem.Instance.AddReaction(dealAttackGA);

        yield return null;
    }
}
