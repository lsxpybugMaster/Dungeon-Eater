using System.Collections.Generic;
using UnityEngine;

//多语言系统
public enum Language
{
    EN,
    CN
}

//本地化数据结构
[System.Serializable]
public class LocalizationEntry
{
    //NOTE: 多语言的核心是使用Key索引不同语言的文本内容
    //命名规范：模块_对象_属性
    public string Key;
    public string EN;
    public string CN;
}


/*
    启动方法 
    从外部读取至内部 List<LocalizationEntry> entries = LocalizationLoader.Load(csvFile);
    解析成字典类型   LocalizationManager.Instance.Load(entries);
 */
public class LocalizationManager : PersistentSingleton<LocalizationManager>
{
    public Language CurrentLanguage = Language.CN;

    private Dictionary<string, string> table = new();

    private List<LocalizationEntry> entries; 

    protected override void Awake()
    {
        //先继承跨场景单例的Awake
        base.Awake();

        //从资源文件中加载CSV
        TextAsset csv = Resources.Load<TextAsset>("Localization/localization");
        
        //转换为序列化结构
        entries = LocalizationLoader.Load(csv);
        
        //储存至字典中
        Load(entries);
    }


    public void Load(List<LocalizationEntry> entries)
    {
        table.Clear();

        foreach (var e in entries)
        {
            string value = CurrentLanguage == Language.EN ? e.EN : e.CN;
            table[e.Key] = value;
        }
    }

    public void ChangeLanguage(Language lang)
    {
        CurrentLanguage = lang;
        //根据语言改变
        Load(entries);
    }

    public string Get(string key)
    {
        if (table.TryGetValue(key, out var value))
            return value;

        //如果找不到对应的本地化文本,则返回key本身
        //Debug.LogWarning("Missing localization key: " + key);
        return key;
    }

    public string Get(string key, params object[] args)
    {
        string str = Get(key);
        return string.Format(str, args);
    }

    //public void ChangeLanguage(Language lang)
    //{
    //    CurrentLanguage = lang;
    //    Reload();
    //    RefreshAllTexts();
    //}

    void Reload()
    {
        //重新加载CSV
    }

    void RefreshAllTexts()
    {
        foreach (var t in FindObjectsOfType<LocalizedTMPText>())
            t.Refresh();
    }
}