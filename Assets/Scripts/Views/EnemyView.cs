using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//TODO: 重构代码,EnemyView现在管理EnemyAI,违背单一职责原则
public class EnemyView : CombatantView
{
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text intendText;

    [field: SerializeField] public EnemyAI EnemyAI { get; private set; }

    private EnemyIntend perkIntend;

    public void Setup(EnemyData enemyData, EnemyCombatant enemyCombatant)
    {
        //别忘记调用基类的初始化方法
        base.Setup(enemyData.Image, enemyCombatant);
       
        //对EnemyAI进行依赖注入
        EnemyAI.BindEnemy(this, enemyData.ConditionedIntendTable, enemyData.RandomIntendTable);

        //基于事件的IoC
        EnemyAI.OnEnemyAIUpdated += UpdateIntendText;
        EnemyAI.OnEnemyAIDone += ClearIntendText;

        UpdateAttackText(null);

        perkIntend = enemyData.PerkIntend;
        PerkSetup(perkIntend);   
      
    }

    //敌人天赋的初始化
    private void PerkSetup(EnemyIntend perkIntend)
    {
        if (perkIntend == null)
            return;
        ActionSystem.SubscribeReaction<BattleSetupGA>(PerkReaction, ReactionTiming.PRE);
    }    

    private void PerkReaction(BattleSetupGA ga)
    {
        GameAction perkGA = perkIntend.GetGameAction(this);
        ActionSystem.Instance.AddReaction(perkGA);
    }


    //TODO: 是否需要OnDisable也加入此逻辑？(如果使用对象池)
    private void OnDestroy()
    {
        EnemyAI.OnEnemyAIUpdated -= UpdateIntendText;
    }

    private void UpdateAttackText(string attackInfoStr)
    {
        if (string.IsNullOrEmpty(attackInfoStr))
            attackText.text = "";
        else
            attackText.text = "< " + attackInfoStr + " >";
    }

    /// <summary>
    /// 在这里更新敌人意图信息
    /// </summary>
    /// <param name="enemyIntend"></param>
    private void UpdateIntendText(EnemyIntend enemyIntend)
    {
        intendText.text = enemyIntend.Skill.ToString();

        string dmgStr = enemyIntend.Skill switch
        {
            EnemySkill.LightHit => ((EnemyCombatant)M).LightAttackPowerStr,
            EnemySkill.HeavyHit => ((EnemyCombatant)M).HeavyAttackPowerStr,
            EnemySkill.Attack => enemyIntend is AttackIntend attackIntend ? attackIntend.GetDmgStr : $"Error: {enemyIntend.ToString()}",
            EnemySkill.FixedHit => ((EnemyCombatant)M).FixedAttackPower.ToString(),
            EnemySkill.SpecialAtk => enemyIntend is IHaveDmgInfo intend ? intend.dmgStrInfo : "Error of IHaveDmgInfo",
            _ => ""
        };

        UpdateAttackText(dmgStr);
    }

    private void ClearIntendText()
    {
        intendText.text = "";
    }
}
