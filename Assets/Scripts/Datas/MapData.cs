using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// 将MapData分为几个关卡,每个关卡是一个level
/// </summary>
[System.Serializable]
public class LevelData
{
    /// <summary>
    /// 存储关卡数据 UDLR
    /// </summary>
    /*
        RRRRD
        U   D
        ULLLL
     */
    [Tooltip("关卡配置代码格式:L4R4U2D3, 也可使用原始格式")]
    public string levelSetCode;
    //NOTE: 该字符串在编辑器中便被解析成为可分析的字符串
    public string levelInfostr;

    public int shopGrids;
}

/// <summary>
/// 地图配置数据类,不要对其进行数值改动,而是使用MapState接入该部分
/// </summary>
[CreateAssetMenu(menuName = "Data/Map")]
public class MapData : ScriptableObject
{
    [field: SerializeReference, SR] public LevelData[] levels { get; private set; }


#if UNITY_EDITOR
    // 在编辑器中修改字段时自动触发,以下功能仅是为了在编辑器中编辑方便
    private void OnValidate()
    {
        if (levels == null) return;
        foreach (var level in levels)
        {
            if (level != null)
            {
                level.levelInfostr = StringUtil.ParseLevelString(level.levelSetCode);
            }
        }
    }
#endif
}
