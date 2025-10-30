using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责初始化地图,以及存储一些地图配置信息(在编辑器中编辑的)
/// </summary>
public class MapControlSystem : Singleton<MapControlSystem>
{
    private bool hasSetup = false;

    private MapState mapState;

    [SerializeField] private MapViewCreator mapViewcreator;

    [Header("地图的相关编辑器配置数据")]
    [SerializeField] private float gridInterval;
    [SerializeField] private float gridSize;

    public MapViewCreator MapViewCreator => mapViewcreator;

    //封装需要暴露的字段为属性
    public float GridInterval => gridInterval;
    public float GridSize => gridSize;
   
    void Start()
    {
        //防止初次进入Battle场景时该脚本早于GameManager初始化,导致重复执行setup函数
        if (!hasSetup)
            SetupMap();
    }


    private void OnEnable()
    {
        //确保该系统晚于GameManager创建,防止获取不到持久数据
        GameManager.OnGameManagerInitialized += SetupMap;
    }

    private void OnDisable()
    {
        GameManager.OnGameManagerInitialized -= SetupMap;
    }

    private void SetupMap()
    {
        Debug.Log("初始化地图");
        mapState = GameManager.Instance.MapState;
        MapViewCreator.CreateMap(mapState.Map);
        hasSetup = true;
    }

}
