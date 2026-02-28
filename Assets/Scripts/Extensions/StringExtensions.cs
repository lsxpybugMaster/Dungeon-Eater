using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 目前提供一些辅助字符串处理函数
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 使用命名占位符格式化字符串, 例如: "Hello {name}, you have {count} new messages.".FormatNamed(new Dictionary<string, object> { {"name", "Alice"}, {"count", 5} })
    /// </summary>
    /// <param name="template">模板字符串</param>
    /// <param name="values">控制命名占位符</param>
    /// <returns></returns>
    public static string FormatNamed(this string template, Dictionary<string, object> values)
    {
        if (values == null || values.Count == 0)
        {
            throw new ArgumentException("字典不能为空", nameof(values));
        }

        string result = template;
        foreach (var pair in values)
        {
            if (!template.Contains("{" + pair.Key + "}"))
            {
                if (pair.Key == "")
                    throw new KeyNotFoundException($"键值为空, 可能没有在数据中配置键值?");
                throw new KeyNotFoundException($"键 '{pair.Key}' 在模板字符串中未找到");
            }

            result = result.Replace("{" + pair.Key + "}", pair.Value?.ToString());
        }
        return result;
    }


    #region 改变字符串颜色

    // 指定颜色
    public static string ColorTo(this string str, Color color)
    {
        string hexColor = ColorUtility.ToHtmlStringRGBA(color);
        return $"<color=#{hexColor}>{str}</color>";
    }

    // 使用颜色名称或十六进制
    public static string ColorTo(this string str, string colorCode)
    {
        return $"<color={colorCode}>{str}</color>";
    }
    #endregion
}
