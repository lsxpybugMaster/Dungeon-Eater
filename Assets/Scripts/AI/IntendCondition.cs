using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//决定敌人意图的条件
[Serializable] //使得能够被编辑器解析(在插件作用下)
public abstract class IntendCondition
{
    //根据敌人的相关信息,进行判定
    public abstract bool Evaluate(EnemyView enemy);
}
