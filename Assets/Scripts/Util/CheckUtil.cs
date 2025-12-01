using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Result
{
    GiantFail,
    Failure,
    Success,
    GiantSuccess
}

//方便UI提取
//public struct AttackResult
//{
//    public int raw;
//    public int add;
//    public int sub;
//}

//处理检定
public static class CheckUtil
{
    /// <summary>
    /// 进行攻击检定: 1d20 + 熟练项 - 目标敏捷项 == 攻击掷骰
    /// </summary>
    /// <param name="proficiency">攻击者熟练项</param>
    /// <param name="flexbility">目标的敏捷值</param>
    /// <returns>返回结果</returns>

    public static Result AttackRoll(CombatantView caster, CombatantView target)
    {

        int attackDice = DiceRollUtil.D20();
        //大成功
        if (attackDice == 20)
        {
            BattleInfoUI.Instance.AddGiantSuccessResult(attackDice, caster);
            return Result.GiantSuccess;
        }
        if (attackDice == 1)
        {
            BattleInfoUI.Instance.AddGiantFailedResult(attackDice, caster);
            return Result.Failure; //大失败不会对玩家造成额外影响
        }

        int add = caster.M.Proficiency;
        int sub = target.M.Flexbility;
        int finalAmount = attackDice + add - sub;
        
        if (attackDice == 1 || finalAmount < 10)
        {
            BattleInfoUI.Instance.AddFailedResult(attackDice, add, sub, 10, "20", caster);
            return Result.Failure;
        }
        else 
        {
            BattleInfoUI.Instance.AddSuccessResult(attackDice, add, sub, 10, "20", caster);
            return Result.Success;
        }

    }
}
