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

    [field: SerializeReference, SR] public List<EnemyIntend> IntendTable { get; private set; }

    //下面代码的用作基本测试
    public void BindEnemy(EnemyView enemy)
    {
        this.enemy = enemy;
    }
    
    public GameAction GetEnemyIntend()
    {
        if (enemy == null)
        {
            Debug.LogError("EnemyAI没有绑定enemy!");
        }

        //使用外部配置的IntendTable的可配置"简单行为树"
        int idx = Random.Range(0, IntendTable.Count);
        return IntendTable[idx].GetGameAction(enemy);

        //GameAction emenyAction;
        ////散装行为树
        //int randomActionCode = Random.Range(0, 100);
        //if (randomActionCode < 50)
        //{
        //    emenyAction = new AttackHeroGA(enemy);
        //}
        //else
        //{
        //    emenyAction = new AddStatusEffectGA(StatusEffectType.AMROR, 5, new(){ enemy });
        //}
        //return emenyAction;
    }
}
