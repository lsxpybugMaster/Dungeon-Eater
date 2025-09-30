using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : Effect
{
    [SerializeField] private int damageAmount;
    /// <summary>
    /// ���ⲿ����Ч����Ӧ��Ŀ��
    /// </summary>
    /// <param name="targets"></param>
    /// <returns></returns>
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        //����GA������
        DealDamageGA dealDamageGA = new(damageAmount, targets, caster);

        return dealDamageGA;
    }
}
