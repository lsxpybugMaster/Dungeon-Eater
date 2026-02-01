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

    //同时准备一个敌人生成器,将敌人提前保存在房间中(为了确定性生成)
    private EnemyGroupGenerator enemyGroupGenerator;

    public MapGenerator(MapData mapData)
    {
        baseData = mapData;
        //以后使用"Map"索引随机流;
        rng = GameManager.Instance.RogueController.GetStream("Map");

        enemyGroupGenerator = new();
    }

    /// <summary>
    /// 生成基本的随机地图,先以一维数组形式返回
    /// //TODO: 目前地图的形状是固定的,所以生成的部分有随机有预设 
    /// //OPTIMIZE: 使用了洗牌算法
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <returns></returns>
    public List<MapGrid> GenerateLevel(int levelIndex)
    {
        var levelConfig = baseData.levels[levelIndex];

        string mapInfo = levelConfig.levelInfostr;

        // 分析各个房间格子数量
        int totalGrids = mapInfo.Length;

        // 封装房间格子数量信息
        RoomCounts cnt = levelConfig.roomCounts;
        
        // 格子信息数组
        List<MapGrid> grids = new(totalGrids);
        // 先默认所有格子是Enemy(考虑到该格子最多)
        for (int i = 0; i < totalGrids; i++)
        {
            grids.Add(new MapGrid
            {
                gridIndex = i,
                gridType = GridType.Enemy,
                nextDirection = mapInfo[i],
            });
        }

        // 构造索引数组,执行洗牌算法,获取打乱后的索引数组
        List<int> indices = new(totalGrids);
        for (int i = 0; i < totalGrids; i++)
            indices.Add(i);

        Shuffle(indices, rng);

        // 分配房间
        int cursor = 0; //指向当前分配的房间索引
        Assign(grids, indices, ref cursor, cnt.shopRoomNumbers,  GridType.Shop);
        Assign(grids, indices, ref cursor, cnt.restRoomNumbers,  GridType.Rest);
        //Assign(grids, indices, ref cursor, cnt.blessRoomNumbers, GridType.Event);
        Assign(grids, indices, ref cursor, cnt.bossRoomNumbers,  GridType.Boss);
        //auto Assign Eneny Room

        for (int i = 0; i < totalGrids; i++)
        {
            if (grids[i].gridType == GridType.Enemy)
            {
                grids[i].roomEnemies = enemyGroupGenerator.GetEnemyGroup(Config.Instance.difficultScore);
            }
            else if (grids[i].gridType == GridType.Boss)
            {
                //非Enemy级别敌人无需组装
                grids[i].roomEnemies = enemyGroupGenerator.GetBossGroup();
            }
        }

        return grids;       
    }

    /// <summary>
    /// Fisher–Yates洗牌 (确定性由rng保证)
    /// 只操作索引序列
    /// </summary>
    private static void Shuffle(List<int> list, System.Random rng)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    /// <summary>
    /// 从洗牌后的索引中分配指定数量的房间
    /// </summary>
    private static void Assign(
        List<MapGrid> grids,
        List<int> indices,
        ref int cursor,
        int count,
        GridType type)
    {
        for (int i = 0; i < count; i++)
        {
            int idx = indices[cursor++];
            grids[idx].gridType = type;
        }
    }
}
