using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//TODO: 重构代码,EnemyView现在管理EnemyAI,违背单一职责原则
public class EnemyView : CombatantView
{
    [SerializeField] private TMP_Text attackText;

    [field: SerializeField] public EnemyAI EnemyAI { get; private set; }

    public int AttackPower { get; set; }

    public void Setup(EnemyData enemyData)
    {
        AttackPower = enemyData.AttackPower;
        UpdateAttackText();
        //别忘记调用基类的初始化方法
        SetupBase(enemyData.Health, enemyData.Health, enemyData.Image);
       
        //对EnemyAI进行依赖注入
        EnemyAI.BindEnemy(this, enemyData.IntendTable);
    }

    private void UpdateAttackText()
    {
        attackText.text = "ATK: " + AttackPower;
    }    
}
