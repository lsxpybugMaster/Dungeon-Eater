using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定义State相关类的基本功能
/// </summary>
public interface IDataState
{
    //从Unity提供的Resources文件获取数据信息
    void LoadDataFromResources(string path);
}
