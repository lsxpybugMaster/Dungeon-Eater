using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField] private TMP_Text attackText;

    public int AttackPower { get; set; }
    public void Setup(EnemyData enemyData)
    {
        AttackPower = enemyData.AttackPower;
        UpdateAttackText();
        //别忘记调用基类的初始化方法
        SetupBase(enemyData.Health, enemyData.Image);
    }

    private void UpdateAttackText()
    {
        attackText.text = "ATK: " + AttackPower;
    }    
}
