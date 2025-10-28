using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    随机地图生成器,提供接口进行随机数据生成
    数据流向:    
    MapGenerator生成 -> MapState保存 -> MapController初始化 -> MapCreater建立View
 */
public class MapGenerator
{
    //传入配置数据
    private MapData baseData;
    //从RNGSystem中获取随机数生成器
    private System.Random rng;

    public MapGenerator(MapData mapData)
    {
        baseData = mapData;
        //以后使用"Map"索引随机流;
        rng = GameManager.Instance.RogueController.GetStream("Map");
    }

    /// <summary>
    /// 生成基本的随机地图,先以一维数组形式返回
    /// //TODO: 目前地图的形状是固定的,所以生成的部分有随机有预设 
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <returns></returns>
    public List<MapGrid> GenerateLevel(int levelIndex)
    {
        var levelConfig = baseData.levels[levelIndex];

        string mapInfo = levelConfig.levelInfostr;
        Debug.Log(mapInfo);

        int totalGrids = mapInfo.Length;

        int shopGrids = levelConfig.shopGrids;

        List<MapGrid> grids = new(totalGrids);
        //后面生成时判重
        HashSet<int> used = new();

        //首先做初始化: 默认房间类型,以及匹配方向
        for (int i = 0; i < totalGrids; i++)
        {
            grids.Add(new MapGrid
            {
                gridIndex = i,
                gridType = GridType.Enemy,
                nextDirection = mapInfo[i],
            });
        }

        //IDEA: 继续使用洗牌算法?
        //后续的生成方法
        for (int i = 0; i < shopGrids; i++)
        {
            int idx;
            //防止重复
            do
            {
                //使用Map随机流生成随机数
                idx = rng.Next(0, totalGrids);
            }
            while (used.Contains(idx));
            used.Add(idx);
            grids[idx].gridType = GridType.Shop;
        }

        return grids;
    }
}
