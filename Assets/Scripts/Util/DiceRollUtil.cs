using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

/// <summary>
/// 提供各类dnd掷骰结果
/// </summary>
public static class DiceRollUtil
{
    //C# 原生随机
    private static System.Random rng = new System.Random();

    public static int D(int x)
    {
        if (x <= 0)
            Debug.LogWarning($"D(x) 收到非法字符 {x}");
        int res = rng.Next(1, x + 1);
        return res;
    }

    //包装两个常用函数
    public static int D6() 
    {
        return D(6); //左闭右开     
    }

    public static int D20()
    {
        return D(20); //左闭右开
    }

    //2d10 + 1d6 => call 2 * D10() + D6()
    //public static int DfromString(string command)
    //{

    //}
       
}
