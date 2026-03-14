using System.Collections.Generic;
using UnityEngine;

public class LocalizationLoader
{
    //解析CSV文件,返回成序列化后的List格式
    public static List<LocalizationEntry> Load(TextAsset csv)
    {
        List<LocalizationEntry> list = new();

        string[] lines = csv.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            var cols = ParseCSVLine(lines[i].Trim());

            if (cols.Length < 3) continue;

            list.Add(new LocalizationEntry
            {
                Key = cols[0],
                EN = cols[1],
                CN = cols[2]
            });
        }

        return list;
    }

    private static string[] ParseCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        System.Text.StringBuilder currentField = new System.Text.StringBuilder();

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                // 处理双引号转义（两个连续的双引号表示一个双引号字符）
                if (i + 1 < line.Length && line[i + 1] == '"')
                {
                    currentField.Append('"');
                    i++; // 跳过下一个引号
                }
                else
                {
                    inQuotes = !inQuotes; // 切换引号状态
                }
            }
            else if (c == ',' && !inQuotes)
            {
                // 不在引号内遇到逗号，结束当前字段
                result.Add(currentField.ToString());
                currentField.Clear();
            }
            else
            {
                // 普通字符
                currentField.Append(c);
            }
        }

        // 添加最后一个字段
        result.Add(currentField.ToString());

        return result.ToArray();
    }
}