using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 将MapData分为几个关卡,每个关卡是一个level
/// </summary>
[System.Serializable]
public class LevelData
{
    public int maxGrids;
    public int shopGrids;
}

/// <summary>
/// 地图配置数据类,不要对其进行数值改动,而是使用MapState接入该部分
/// </summary>
[CreateAssetMenu(menuName = "Data/Map")]
public class MapData : ScriptableObject
{
    [field: SerializeReference, SR] public LevelData[] levels { get; private set; }

}
