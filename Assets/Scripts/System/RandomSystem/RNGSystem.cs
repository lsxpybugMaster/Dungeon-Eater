using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Random Number Generator
/// 全局随机数生成器,用于实现基于种子的可控随机
/// 分配随机流给不同的系统
/// </summary>
//DISCUSS: 本质上是管理多个随机数发生器(但是种子都是完全可控的,并且由MasterSeed全权决定)
public class RNGSystem
{
    //这种全局性的固定种子随机生成器使用C#原生的好, Unity.Random适合那些非持久化的随机生成器
    private System.Random masterRng; //随机数生成器

    /// <summary>
    /// 利用字典,通过字符串索引分支流
    /// </summary>
    private Dictionary<string, System.Random> subStreams = new();

    public int MasterSeed { get; private set; }

    public RNGSystem(int seed)
    {
        MasterSeed = seed;
        //使用种子随机化随机数发生器
        masterRng = new System.Random(seed);
    }

    /// <summary>
    /// 依据随机流的名字获取随机数发生器
    /// </summary>
    /// <param name="streamName"></param>
    /// <returns></returns>
    public System.Random GetStream(string streamName)
    {
        // 如果有则直接返回,否则创建新的
        if (subStreams.TryGetValue(streamName, out var existing))
            return existing;

        //由MasterSeed生成分支subseed,这些seed都是确定的
        int subSeed = masterRng.Next(0, int.MaxValue);
        //由分支subseed获取
        var subRng = new System.Random(subSeed);
        subStreams[streamName] = subRng;
        return subRng;
    }
}
