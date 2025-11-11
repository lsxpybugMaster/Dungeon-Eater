using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    类似行为树? 存储敌人的对应GameAction(或相关封装)
    依据逻辑(传入函数委托?) 执行相关GameAction
 */
public class EnemyAI : MonoBehaviour
{
    [field: SerializeReference, SR] public List<EnemyIntend> IntendTable { get; private set; }
   
}
