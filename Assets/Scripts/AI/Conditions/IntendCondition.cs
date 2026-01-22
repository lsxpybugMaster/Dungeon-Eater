using System;
using System.Collections.Generic;

public enum Relation
{
    less,
    less_equal,
    greater, 
    greater_equal,
    equal,
}

//决定敌人意图的条件
[Serializable] //使得能够被编辑器解析(在插件作用下)
public abstract class IntendCondition
{
    //static的字典存储辅助类
    protected static readonly Dictionary<Relation, Func<int, int, bool>> Compare = new()
    {
        { Relation.less,         (a, b) => a < b },
        { Relation.equal,        (a, b) => a == b },
        { Relation.greater,      (a, b) => a > b },
        { Relation.less_equal,   (a, b) => a <= b},
        { Relation.greater_equal,(a, b) => a >= b }
    };


    //根据敌人的相关信息,进行判定
    public abstract bool Evaluate(EnemyView enemy);
}
