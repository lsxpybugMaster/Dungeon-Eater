using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : Effect
{
    [SerializeField] private int damageAmount;
    public override GameAction GetGameAction()
    {
        List<CombatantView> targets = new(EnemySystem.Instance.Enemies);
        //创建GA并返回
        DealDamageGA dealDamageGA = new(damageAmount, targets);

        return dealDamageGA;
    }
}
