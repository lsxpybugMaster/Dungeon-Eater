using UnityEngine;

/// <summary>
/// Key   : RewardType
/// Value : RewardData
/// </summary>
[CreateAssetMenu(menuName = "DataBase/RewardDataBase")]
public class RewardDataBase : DataBase<RewardType, RewardData>
{
    // --------------------- 单例访问 ---------------------
    // 这块逻辑提取成基类需要泛型,Resources.Load使用泛型会有问题
    private static RewardDataBase _instance;
    public static RewardDataBase Instance
    {
        get
        {
            if (_instance == null)
            {
                //路径不区分大小写, 不包括文件的扩展名
                _instance = Resources.Load<RewardDataBase>("DB/RewardDatabase");
                if (_instance == null)
                    Debug.LogError("RewardDatabase.asset not found in Resources folder!");
                else
                    _instance.Init();
            }
            return _instance;
        }
    }

    // --------------------- 获取接口 ---------------------

    /// <summary> 依据奖励类型获取对应数据 </summary>
    /// 传入的函数形式 bool func(CardData)
    public static RewardData GetRewardDataByType(RewardType rewardType)
    {
        return Instance.idLookup.TryGetValue(rewardType, out var reward)
            ? reward 
            : null;
    }
}
