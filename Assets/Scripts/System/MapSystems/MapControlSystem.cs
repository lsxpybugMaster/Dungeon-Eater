using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责初始化地图,以及存储一些地图配置信息(在编辑器中编辑的)
/// </summary>
//IMPORTANT: 整个Map的底层
public class MapControlSystem : Singleton<MapControlSystem>
{
    private bool hasSetup = false;

    private MapState mapState;
    private MapDicesSystem dicesSystem;

    [SerializeField] private MapViewCreator mapViewcreator;

    [Header("地图的相关编辑器配置数据")]
    [SerializeField] private float gridInterval;
    [SerializeField] private float gridSize;
    [SerializeField] private float mapDiceMoveSpeed;
    private float step;

    public MapViewCreator MapViewCreator => mapViewcreator;
    

    //封装需要暴露的字段为属性
    public float GridInterval => gridInterval;
    public float GridSize => gridSize;
    public float MapDiceMoveSpeed => mapDiceMoveSpeed;

    /// <summary>
    /// 实际生成/在格子中移动的距离
    /// </summary>
    public float Step => step;

    protected override void Awake()
    {
        base.Awake();
        step = gridSize + gridInterval;
        if (mapDiceMoveSpeed <= 0)
            Debug.LogError("非法的或未初始化mapDiceMoveSpeed");
    }

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
        mapState = GameManager.Instance.MapState;
        dicesSystem = MapDicesSystem.Instance;
        //每次重新进入都需要生成地图,同时初始化骰子位置
        mapViewcreator.CreateMapWithDice(mapState.Map, mapState.MapDiceList);
     
        dicesSystem.SetUp(mapState.MapDiceList);
        hasSetup = true;
    }
}
