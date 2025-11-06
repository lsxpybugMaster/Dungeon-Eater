using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
    保存持久化数据
    MapControlSystem直接读取并修改内容
 */
public class MapState : BaseState<MapData>
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

    //IMPORTANT: 我们的持久化地图就在里面,存储的都是Model(MVC=>M)
    public List<MapGrid> Map {  get; private set; } //当前的地图
    public List<MapDice> MapDiceList { get; set; } //当前地图对应的骰子,可以被Map编辑
 
    //------------------------持久化数据---------------------------
    //NOTE: 全局仅执行一次
    public MapState()
    {
        //自己去获取数据文件,不再由GameManager管理
        LoadDataFromResources("MapData/MapData");

        currentLevel = 0;
        currentStep = 0;
        initNewLevel(currentLevel);

        GenerateMap();
        GenerateDiceState();
    }

    public void initNewLevel(int curLevel)
    {
        mapDices = BaseData.levels[curLevel].mapDices;
    }

    //生成动态地图数据
    private void GenerateMap()
    {
        var generator = new MapGenerator(BaseData);
        Map = generator.GenerateLevel(currentLevel);
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
}
