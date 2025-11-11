using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//包装GameAction,作为敌人意图,成为AI行为序列的一个节点

[Serializable]
public abstract class EnemyIntend 
{
    //传入敌人,返回对应的Action
    public abstract GameAction GetGameAction(EnemyView enemy);
}
