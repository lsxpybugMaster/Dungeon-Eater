using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

    //10 + 2d10 + 1d6 => 10 + 2 * D10() + D6()
    public static int DfromString(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
            return 0;

        // 去空格方便处理
        command = command.Replace(" ", "").ToLower();

        int total = 0;

        // 通过正则拆成 + 和 - 的片段
        var tokens = Regex.Split(command, @"(?=[+-])");

        foreach (var token in tokens)
        {
            if (string.IsNullOrWhiteSpace(token))
                continue;

            int sign = 1;
            string t = token;

            // 处理前缀符号
            if (t[0] == '+')
                t = t.Substring(1);
            else if (t[0] == '-')
            {
                sign = -1;
                t = t.Substring(1);
            }

            // 识别骰子格式 n d x
            var match = Regex.Match(t, @"^(\d*)d(\d+)$");

            if (match.Success)
            {
                // 解析出 n 与 x
                int count = string.IsNullOrEmpty(match.Groups[1].Value)
                            ? 1
                            : int.Parse(match.Groups[1].Value);

                int dice = int.Parse(match.Groups[2].Value);

                int diceSum = 0;
                for (int i = 0; i < count; ++i)
                {
                    diceSum += D(dice);
                }

                total += sign * diceSum;
            }
            else
            {
                // 普通数字
                if (int.TryParse(t, out int num))
                {
                    total += sign * num;
                }
                else
                {
                    Debug.LogError($"无法解析 DnD 字符串片段: {token}");
                }
            }
        }

        return total;
    }
}
