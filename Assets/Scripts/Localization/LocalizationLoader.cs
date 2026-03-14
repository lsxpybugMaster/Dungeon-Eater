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
            var cols = lines[i].Split(',');

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
}