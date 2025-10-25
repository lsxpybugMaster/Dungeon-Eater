using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapState 
{
    //如果需要地图配置文件,在这里加载

    //保存一份MapData
    public MapData MapData { get; private set; }

    //------------------------动态数据---------------------------
    public int currentLevel { get; private set; }
    public int currentStep { get; private set; }

    //------------------------持久化数据---------------------------
    public int LevelSize => MapData.levels[currentLevel].maxGrids;

    public MapState()
    {
        //自己去获取数据文件,不再由GameManager管理
        LoadDataFromResources("MapData/MapData");

        currentLevel = 0;
        currentStep = 0;
    }

    void LoadDataFromResources(string path)
    {
       ResourceLoader.LoadSafe<MapData>(path);
    }
}
