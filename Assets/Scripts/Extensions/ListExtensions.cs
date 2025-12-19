using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//静态工具类,无需挂载脚本,直接使用
public static class ListExtensions 
{
    //从list中抽取数据的泛型类
    //this 关键字指明这是一个扩展方法,通过myList.Draw()调用
    public static T Draw<T>(this List<T> list)
    {
        //default 关键字返回T类型的默认值
        if (list.Count == 0) return default;
        int r = UnityEngine.Random.Range(0, list.Count);
        T t = list[r];
        list.Remove(t);
        return t;
    }

    public static T GetRandom<T>(this List<T> list)
    {
        if (list.Count == 0) return default;
        int r = UnityEngine.Random.Range(0, list.Count);
        return list[r];
    }

    // 使用 System.Random（可控随机）
    public static T GetRandom<T>(this List<T> list, System.Random rng)
    {
        if (list == null || list.Count == 0)
            return default;

        if (rng == null)
            throw new ArgumentNullException(nameof(rng));

        int r = rng.Next(0, list.Count); 
        return list[r];
    }

    // 从列表中选择n个数据（允许重复）并返回
    public static List<T> GetRandomN<T>(this List<T> list, int count, bool allowDuplicates = true)
    {
        List<T> result = new List<T>();

        // 如果列表为空或请求数量小于等于0，返回空列表
        if (list.Count == 0 || count <= 0)
            return result;

        // 如果允许重复
        if (allowDuplicates)
        {
            for (int i = 0; i < count; i++)
            {
                int r = UnityEngine.Random.Range(0, list.Count);
                result.Add(list[r]);
            }
        }
        else // 不允许重复
        {
            // 如果请求数量超过列表元素数量，限制为列表元素数量
            int drawCount = Mathf.Min(count, list.Count);

            // 创建一个临时列表副本以避免修改原始列表
            List<T> tempList = new List<T>(list);

            for (int i = 0; i < drawCount; i++)
            {
                int r = UnityEngine.Random.Range(0, tempList.Count);
                result.Add(tempList[r]);
                tempList.RemoveAt(r);
            }
        }

        return result;
    }
}
