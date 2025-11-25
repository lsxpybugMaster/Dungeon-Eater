using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 与DealDamageEffect类似,处理骰子字符串，并解析对应伤害
/// </summary>
public class DealRandomDmgEffect : Effect
{
    /// <summary>
    /// 骰子判定字符串
    /// </summary>
    [SerializeField] private string diceString; 
    /// <summary>
    /// 从外部传入效果对应的目标
    /// </summary>
    /// <param name="targets"></param>
    /// <returns></returns>
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        DealAttackGA dealAttackGA = new(diceString, targets, caster);
        return dealAttackGA;
        
        
        //int dmg = DiceRollUtil.DfromString(diceString);

        ////添加检定结果到UI显示中
        //BattleInfoUI.Instance.AddThrowResult(dmg, diceString);

        ////创建GA并返回
        //DealDamageGA dealDamageGA = new(dmg, targets, caster);

        //return dealDamageGA;
    }
}
