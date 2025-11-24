public enum CheckType
{
    AbilityCheck,    // 属性检定
    AttackRoll,      // 攻击检定
    SavingThrow,     // 豁免检定
    SkillCheck,      // 技能检定
    DamageRoll,      // 伤害掷骰
}

/// <summary>
/// 更新UI检定信息
/// </summary>
public class UpdateBattleInfoGA : GameAction
{
    public int Roll { get; set; } //掷骰点数

    public int DC { get; set; } //难度等级(有些不需要)

    public string Dice { get; set; } //骰子数 (6d8)

    public CheckType CheckType { get; set; }

    public UpdateBattleInfoGA(CheckType checkType, int roll, int dc, string dice)
    {
        CheckType = checkType;
        Roll = roll;
        DC = dc;
        Dice = dice;
    }
}
