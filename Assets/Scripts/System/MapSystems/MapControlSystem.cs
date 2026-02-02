using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

/// <summary>
/// 负责初始化地图,以及存储一些地图配置信息(在编辑器中编辑的)
/// <see cref="MapGenerator"> 对应的地图生成核心逻辑
/// <see cref="MapState"/> 对应的持久化数据结构
//DISCUSS: 有必要同时负责模式切换系统吗?
/// </summary>
//IMPORTANT: 整个Map的底层
public class MapControlSystem : Singleton<MapControlSystem>, IRequireGameManager
{
    private MapState mapState;
    private MapDicesSystem dicesSystem;

    [Header("管理的子模块")]
    [SerializeField] private MapViewCreator mapViewcreator;
    [SerializeField] private MapUI mapUI;
    [SerializeField] private LevelProgressUI levelProgressUI;
    // [SerializeField] private ChangeRoomSystem changeRoomSystem;

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

    IEnumerator Start()
    {
        //防止初次进入Map场景时该脚本早于GameManager初始化,导致重复执行setup函数
        //if (!hasSetup)
        //    SetupMap();

        yield return this.WaitGameManagerReady(SetupMap);
    }


    private void OnEnable()
    {
        //确保该系统晚于GameManager创建,防止获取不到持久数据
        //GameManager.OnGameManagerInitialized += SetupMap;
    }


    private void OnDisable()
    {
        //GameManager.OnGameManagerInitialized -= SetupMap;
    }


    private void SetupMap()
    {
        mapState = GameManager.Instance.MapState;
        dicesSystem = MapDicesSystem.Instance;
        //每次重新进入都需要生成地图,同时初始化骰子位置
        mapViewcreator.CreateMapWithDice(mapState.Map, mapState.MapDiceList);
        dicesSystem.SetUp(mapState.MapDiceList);

        SetupOtherLogic();
    }

    public void UpdateMapGrid(int index, GridType type)
    {
        mapViewcreator.mapGridViewList[index].UpdateView(type);
    }

    private void SetupOtherLogic()
    {
        levelProgressUI.gameObject.SetActive(true);
    }
}
