using SerializeReferenceEditor;
using System;
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

    //基于事件通知EnemyView (IoC)
    public event Action<EnemyIntend> OnEnemyAIUpdated;
    public event Action OnEnemyAIDone;

    private EnemyIntend enemyNextIntend;

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
    

    /// <summary>
    /// 在该函数中提前获取行为, 用于UI显示, 同时保留行为信息, 到达敌人回合时直接使用
    /// </summary>
    /// <returns></returns>
    protected virtual EnemyIntend DecideEnemyIntend()
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
            if ((IntendNode.Condition == null || IntendNode.Condition.Evaluate(enemy)) && UnityEngine.Random.value < IntendNode.P)
            {
                return IntendNode.Intend;
            }
        }

        //--------------------- 行为判断阶段 ----------------------
        int idx = UnityEngine.Random.Range(0, RandIntendTable.Count);
        return RandIntendTable[idx].Intend;
    }


    /// <summary>
    /// 计算并获取敌人意图,一般不会执行
    /// </summary>
    /// <returns></returns>
    public EnemyIntend GetPrepareEnemyIntend()
    {
        enemyNextIntend = DecideEnemyIntend();
        //通知父模块
        OnEnemyAIUpdated?.Invoke(enemyNextIntend);
        return enemyNextIntend;
    }


    /// <summary>
    /// 获取计算好的敌人行为,直接执行
    /// 从DecideEnemyIntend中确定了enemyNextIntend,解析出GameAction去具体执行
    /// </summary>
    /// <returns></returns>
    public GameAction GetEnemyAction()
    {
        if (enemyNextIntend == null)
        {
            Debug.LogWarning("enemyNextIntend == null");
            enemyNextIntend = DecideEnemyIntend();
        }
        //通知父模块
        OnEnemyAIDone?.Invoke();
        return enemyNextIntend.GetGameAction(enemy);
    }
}
