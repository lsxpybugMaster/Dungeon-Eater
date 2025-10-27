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

    public List<MapGrid> GetMap => Map;

    //------------------------持久化数据---------------------------

    public MapState()
    {
        //自己去获取数据文件,不再由GameManager管理
        LoadDataFromResources("MapData/MapData");

        currentLevel = 0;
        currentStep = 0;

        GenerateMap();

        //Debug.Log($"SIZE: {Map.Count}");
        //string dbg = "";
        //foreach (var i in Map)
        //{
        //    dbg += i.gridType.ToString() + ',';
        //}
        //Debug.Log(dbg);
        MapViewCreator.Instance.CreateMap(GetMap);
    }

    public void GenerateMap()
    {
        var generator = new MapGenerator(BaseData);
        Map = generator.GenerateLevel(currentLevel);
    }
}
