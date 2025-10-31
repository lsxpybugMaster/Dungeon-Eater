using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 提供随机数生成工具
/// </summary>
public static class RandomUtil
{
    /// <summary>
    /// 在指定范围内生成 n 个不重复的随机整数索引。
    /// </summary>
    /// <param name="min">最小值（包含）</param>
    /// <param name="max">最大值（不包含）</param>
    /// <param name="count">要生成的数量</param>
    /// <returns>不重复索引列表</returns>
    public static List<int> GetUniqueRandomIndexes(int min, int max, int count)
    {
        if (max <= min)
            throw new ArgumentException("max 必须大于 min。");
        if (count > max - min)
            throw new ArgumentException("生成数量不能超过范围长度。");

        List<int> result = new List<int>(count);
        HashSet<int> used = new HashSet<int>();

        while (result.Count < count)
        {
            int value = UnityEngine.Random.Range(min, max);
            if (used.Add(value))
                result.Add(value);
        }

        return result;
    }
}
