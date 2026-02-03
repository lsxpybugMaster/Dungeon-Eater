using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    保存持久化数据
    MapControlSystem直接读取并修改内容
 */
public class MapState : BaseState<MapData>, IOnDestroy
{
    //如果需要地图配置文件,在这里加载

    //保存一份MapData
    //public MapData MapData { get; private set; }
    //NOTE: 现在不需要专门建立data,data保存在BaseState中, 并且不要直接使用!

    //------------------------动态数据---------------------------
    public int currentLevel { get; private set; }
    public int currentStep { get; private set; } //TODO: 暂时未用

    /// <summary>
    /// 当前关卡的mapDice最大数量
    /// </summary>
    public int mapDices { get; private set; }


    //------------------------持久化数据---------------------------
    //IMPORTANT: 我们的持久化地图就在里面,存储的都是Model(MVC=>M)
    public List<MapGrid> Map {  get; private set; } //当前的地图
    public List<MapDice> MapDiceList { get; set; } //当前地图对应的骰子,可以被Map编辑

    // 与动态地图相关功能有关: BOSS 房覆盖敌人房
    private int bossRoomCursor = 0; //下一个BOSS房指向第几个
    private MapSnapshot mapSnapshot;
 
    public MapState(int level)
    {
        //自己去获取数据文件,不再由GameManager管理
        LoadDataFromResources("MapData/MapData");

        currentLevel = level;
        currentStep = 0;
        InitNewLevel(currentLevel);

        GenerateMap();
        GenerateDiceState();

        //订阅事件
        GameManager.Instance.LevelProgress.OnRoundIncreased += ChangeEnemyRoomToBoss;
    }

    //这是我们在非 Mono 脚本中手动定义的
    public void OnDestroy()
    {
        GameManager.Instance.LevelProgress.OnRoundIncreased -= ChangeEnemyRoomToBoss;
    }

    public int GetMaxLevels()
    {
        return BaseData.levels.Length;
    }

    public void InitNewLevel(int curLevel)
    {
        mapDices = BaseData.levels[curLevel].mapDices;
    }

    //生成动态地图数据
    private void GenerateMap()
    {
        var generator = new MapGenerator(BaseData);
        mapSnapshot = generator.GenerateLevel(currentLevel);
        Map = mapSnapshot.Grids;
    }


    public void GenerateDiceState()
    {
        //NOTE: C# new List<T>(n) 只是预分配 capacity 与C++ Vector不同
        if (Map == null || Map.Count == 0)
            Debug.LogError("Cannot find Map or Map Count is 0");

        List<int> diceIndices = RandomUtil.GetUniqueRandomIndexes(0, Map.Count, mapDices);
        
        MapDiceList = new List<MapDice>();

        for (int i = 0; i < diceIndices.Count; i++)
        {
            //注意初始化列表的方法, 注意充分利用构造函数
            var dice = new MapDice(diceIndices[i]);
            MapDiceList.Add(dice);
        }
    }

    /// <summary>
    /// 特殊动态地图功能: 将地图数据替换
    /// 加参数的意义是匹配 Action 委托事件, 实际不用
    /// </summary>
    public void ChangeEnemyRoomToBoss(LevelProgress levelProgress = null)
    {
        var bossGridList = mapSnapshot.BossGridBuffer;
        int len = bossGridList.Count;

        if (bossRoomCursor >= len)
            return;
        
        // 提取代替的信息
        var (index, bossgrid) = bossGridList[bossRoomCursor];

        // 状态变更
        Map[index] = bossgrid;

        // 更新显示层
        MapControlSystem.Instance.UpdateMapGrid(index, GridType.Boss);

        bossRoomCursor++;
    }


}
