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
    public int currentStep { get; private set; }
    //IMPORTANT: 我们的持久化地图就在里面
    public List<MapGrid> Map {  get; private set; } //当前的地图
    public List<MapDiceView> MapDiceList { get; private set; } //当前地图对应的骰子
 
    //------------------------持久化数据---------------------------
    //NOTE: 全局仅执行一次
    public MapState()
    {
        //自己去获取数据文件,不再由GameManager管理
        LoadDataFromResources("MapData/MapData");

        currentLevel = 0;
        currentStep = 0;

        GenerateMap();
        //GenerateDiceState();
    }

    //生成动态地图数据
    private void GenerateMap()
    {
        var generator = new MapGenerator(BaseData);
        Map = generator.GenerateLevel(currentLevel);
    }


    public void GenerateDiceState()
    {
        if (Map == null || Map.Count == 0)
            Debug.LogError("MAPSIZE == 0!!!");
        List<int> diceIndices = RandomUtil.GetUniqueRandomIndexes(0, Map.Count, 3);
        MapDiceList = new List<MapDiceView>(3);
        for (int i = 0; i < MapDiceList.Count; i++)
        {
            MapDiceList[i].Index = diceIndices[i];
        }
    }
}
