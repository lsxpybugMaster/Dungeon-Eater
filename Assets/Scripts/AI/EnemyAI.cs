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

    private List<EnemyIntend> IntendTable { get; set; }

    //下面代码的用作基本测试
    //由EnemyView调用,初始化EnemyAI
    public void BindEnemy(EnemyView enemy, List<EnemyIntend> intendTable)
    {
        this.enemy = enemy;
        //TODO: 目前无法修改IntendTable(否则会修改SO数据)
        IntendTable = intendTable;
    }
    
    public GameAction GetEnemyIntend()
    {
        if (enemy == null)
        {
            Debug.LogError("EnemyAI没有绑定enemy!");
        }

        // 使用外部配置的IntendTable的可配置"简单行为树"
        int idx = Random.Range(0, IntendTable.Count);
        
        // 获取GA时传入了enemy信息,则行为树可以使用enemy数据
        return IntendTable[idx].GetGameAction(enemy);
    }
}
