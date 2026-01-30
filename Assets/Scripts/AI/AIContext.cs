using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//外部配置时使用的key
public enum AI
{
    Turn,
    Charged, //是否进行了蓄力
}

//存储一些上下文,使得AI系统能根据这些环境值进行决策
public class AIContext
{
    private Dictionary<AI, int> counters = new Dictionary<AI, int>
    {
        {AI.Turn, 1},
        {AI.Charged, 0},
    };

    public int Get(AI key) => counters.TryGetValue(key, out var v) ? v : 0;
    //加1
    public void Inc(AI key) => counters[key] = Get(key) + 1;

    public void Inc(AI key, int addNum) => counters[key] = Get(key) + addNum;

    public void Set(AI key, int x) => counters[key] = x;
}
