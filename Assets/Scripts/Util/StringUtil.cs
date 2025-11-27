using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public static class StringUtil
{
    /// <summary>
    /// 解析地图字符串: L3R4 => LLLRRRR
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ParseLevelString(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            Debug.LogError("关卡配置字符串解析后为空!");
            return "";
        }
        // 去除所有空白字符
        str = new string(str.Where(c => !char.IsWhiteSpace(c)).ToArray());
        // 转大写
        str = str.ToUpperInvariant();
        // 兼容原始格式
        if (!str.Any(char.IsDigit))
        {
            return str;
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        char c = ' ';
        int count = 0;
        for (int i = 0; i < str.Length; i++)
        {  
            //字符串解析
            if (char.IsLetter(str[i]))
            {
                if (i > 0 && char.IsLetter(str[i - 1])) count = 1; //兼容连续字母如LRUD
                sb.Append(new string(c, count));
                c = str[i];
                count = 0; 
            }
            else if (char.IsDigit(str[i]))
            {
                count *= 10;
                count += str[i] - '0';
            }
            else
            {
                Debug.LogError($"出现了非法字符{str[i]}");
            }
        }
        
        //里面确保如果最后是单个字母,能够输出
        sb.Append(new string(c, count == 0? 1 : count));

        return sb.ToString();
    }

    //是否是只含数值的字符串
    public static bool IsFixedAmountString(string str)
    {
        return Regex.IsMatch(str.Trim(), @"^\d+$");
    }
}
