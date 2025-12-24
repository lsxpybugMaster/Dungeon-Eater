using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DataBase/RoomDatabase")]
public class RoomDataBase : DataBase<GridType, RoomData>
{
    // --------------------- 单例访问 ---------------------
    // 这块逻辑提取成基类需要泛型,Resources.Load使用泛型会有问题
    private static RoomDataBase _instance;
    public static RoomDataBase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<RoomDataBase>("RoomDataBase");
                if (_instance == null)
                    Debug.LogError("RoomDataBase.asset not found in Resources folder!");
                else
                    _instance.Init();
            }
            return _instance;
        }
    }

    // --------------------- 查询接口 ---------------------

    /// <summary> 通过卡牌ID查找模板 </summary>
    public static RoomData GetRoomData(GridType gridType)
    {
        return Instance.idLookup.TryGetValue(gridType, out var room)
            ? room
            : null;
    }

}
