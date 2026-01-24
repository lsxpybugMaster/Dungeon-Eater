using System;
using System.Collections.Generic;

public enum Relation
{
    less,
    less_equal,
    greater, 
    greater_equal,
    equal,
    if_mod, //回合数模该值为0时执行
    start_and_if_mod //初始执行 + 回合数模该值为0时执行
}

//决定敌人意图的条件
[Serializable] //使得能够被编辑器解析(在插件作用下)
public abstract class IntendCondition
{
    //static的字典存储辅助类
    protected static readonly Dictionary<Relation, Func<int, int, bool>> Compare = new()
    {
        { Relation.less,             (a, b) => a < b },
        { Relation.equal,            (a, b) => a == b },
        { Relation.greater,          (a, b) => a > b },
        { Relation.less_equal,       (a, b) => a <= b},
        { Relation.greater_equal,    (a, b) => a >= b },
        { Relation.if_mod,           (a, b) => a % b == 0},
        { Relation.start_and_if_mod, (a, b) => a == 1 || a % b == 0 }
    };


    //根据敌人的相关信息,进行判定
    public abstract bool Evaluate(EnemyView enemy);
}
