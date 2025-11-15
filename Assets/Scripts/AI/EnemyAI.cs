using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/*
    类似行为树? 存储敌人的对应GameAction(或相关封装)
    依据逻辑(传入函数委托?) 执行相关GameAction
 */
//可以理解为controller?
public class EnemyAI : MonoBehaviour 
{
    protected EnemyView enemy;

    private List<EnemyIntendNode> CondIntendTable { get; set; }
    private List<EnemyIntendNode> RandIntendTable { get; set; }

    //下面代码的用作基本测试
    //由EnemyView调用,初始化EnemyAI
    public void BindEnemy(EnemyView enemy, List<EnemyIntendNode> ConditionedIntendTable, List<EnemyIntendNode> RandomIntendTable)
    {
        this.enemy = enemy;
        //TODO: 目前无法修改IntendTable(否则会修改SO数据)
        CondIntendTable = ConditionedIntendTable;
        RandIntendTable = RandomIntendTable;
    }
    
    public virtual GameAction GetEnemyIntend()
    {
        if (enemy == null)
        {
            Debug.LogError("EnemyAI没有绑定enemy!");
        }

        // 使用外部配置的IntendTable的可配置"简单行为树"
        
        //------------------- 条件行为判断阶段 ----------------------
        // 获取GA时传入了enemy信息,则行为树可以使用enemy数据
        foreach (var IntendNode in CondIntendTable)
        {
            // 判断条件
            if ((IntendNode.Condition == null || IntendNode.Condition.Evaluate(enemy)) && Random.value < IntendNode.P)
            {
                return IntendNode.Intend.GetGameAction(enemy);
            }
        }

        //--------------------- 行为判断阶段 ----------------------
        int idx = Random.Range(0, RandIntendTable.Count);
        return RandIntendTable[idx].Intend.GetGameAction(enemy);
    }
}
