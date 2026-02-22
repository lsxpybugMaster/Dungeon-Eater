using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 管理道具的数据库
/// </summary>
[CreateAssetMenu(menuName = "DataBase/FoodDatabase")]
public class FoodDataBase : DataBase<string, FoodData>
{
    // --------------------- 单例访问 ---------------------
    // 这块逻辑提取成基类需要泛型,Resources.Load使用泛型会有问题
    private static FoodDataBase _instance;

    public static FoodDataBase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<FoodDataBase>("FoodDatabase");
                if (_instance == null)
                    Debug.LogError("FoodDatabase.asset not found in Resources folder!");
                else
                    _instance.Init();
            }
            return _instance;
        }
    }


    // --------------------- 随机接口 ---------------------

    /// <summary> 随机获取一个道具 </summary>
    public static FoodData GetRandomFood(Func<FoodData, bool> filter = null)
    {
        //确定随机的范围
        var pool = filter == null ? Instance.datas : Instance.datas.Where(filter).ToList();
        if (pool.Count == 0) return null;

        int index = UnityEngine.Random.Range(0, pool.Count);
        return pool[index];
    }

}
