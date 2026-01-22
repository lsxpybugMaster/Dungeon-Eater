using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//注意敌人意图显示目前就是这些名字
public enum EnemySkill
{
    LightHit,
    HeavyHit,
    FixedHit, //必中的,伤害固定的攻击
    AddState,
    Defence,
    Heal,
    AddCard, 
}

//包装GameAction,作为敌人意图,成为AI行为序列的一个节点
//IDEA: 注意与Effect进行类别,二者十分相似,都是对GameAction的封装
//需要 [Serializable]

[Serializable]
public abstract class EnemyIntend 
{
    [SerializeField]
    private EnemySkill skill; //具体的细粒度技能,即一个Intend下可能有多个skill

    public EnemySkill Skill => skill;

    //传入敌人,返回对应的Action
    public abstract GameAction GetGameAction(EnemyView enemy);
}
