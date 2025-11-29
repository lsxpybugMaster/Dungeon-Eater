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

    //添加更细粒度的攻击数据
    public int FixedAttackPower { get; set; }
    public string LightAttackPowerStr { get; set; }
    public string HeavyAttackPowerStr { get; set; }


    public void Setup(EnemyData enemyData)
    {
        FixedAttackPower = enemyData.FixedAttack;
        LightAttackPowerStr = enemyData.LightAttackStr;
        HeavyAttackPowerStr = enemyData.HeavyAttackStr;
             
        UpdateAttackText();
        //别忘记调用基类的初始化方法
        base.Setup(enemyData.Health, enemyData.Health, enemyData.Image);
       
        //对EnemyAI进行依赖注入
        EnemyAI.BindEnemy(this, enemyData.ConditionedIntendTable, enemyData.RandomIntendTable);

        //基于事件的IoC
        EnemyAI.OnEnemyAIUpdated += UpdateIntendText;
        EnemyAI.OnEnemyAIDone += ClearIntendText;
    }

    //TODO: 是否需要OnDisable也加入此逻辑？(如果使用对象池)
    private void OnDestroy()
    {
        EnemyAI.OnEnemyAIUpdated -= UpdateIntendText;
    }

    private void UpdateAttackText()
    {
        attackText.text = "ATK: " + FixedAttackPower;
    }

    private void UpdateIntendText(EnemyIntend enemyIntend)
    {
        intendText.text = enemyIntend.Skill.ToString();
    }

    private void ClearIntendText()
    {
        intendText.text = "";
    }
}
