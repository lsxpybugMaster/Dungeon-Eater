using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DataBase/StatusDatabase")]
public class StatusDatabase : DataBase<StatusEffectType, StatusData>
{
    // --------------------- 单例访问 ---------------------
    private static StatusDatabase _instance;
    public static StatusDatabase I
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<StatusDatabase>("DB/StatusDatabase");
                if (_instance == null)
                    Debug.LogError("StatusDataBase.asset not found in Resources folder!");
                else
                    _instance.Init();
            }
            return _instance;
        }
    }

    // --------------------- 查询接口 ---------------------

    /// <summary> 通过卡牌ID查找模板 </summary>
    public static StatusData GetStatusData(StatusEffectType effectType)
    {
        return I.idLookup.TryGetValue(effectType, out var e)
            ? e
            : null;
    }

}