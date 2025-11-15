using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//参考CardData的设计
[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    // 基本数据
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int AttackPower { get; private set; }

    // AI逻辑数据
    [field: SerializeReference, SR]
    public List<EnemyIntendNode> ConditionedIntendTable { get; private set; }


    [field: SerializeReference, SR]
    public List<EnemyIntendNode> RandomIntendTable { get; private set; }
    /*
        简单行为树逻辑:
        条件行为: 按顺序判断，依次执行直到第一个事件执行成功
        随机行为: 如果条件行为没有选出事件,则在随机行为中按概率抽取行为(概率和为1)
     */
}
