using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 我们需要动态地图,然而 MapGenerator 是一次性的,所以需要返回相关数据给state供其后期逻辑
/// </summary>
public class MapSnapshot
{
    public List<MapGrid> Grids; //最基本的地图

    //tuple含义 (Boss覆盖对应的id, 具体的id信息)
    public List<(int, MapGrid)> BossGridBuffer; //预计算好的Boss信息,与enemy按位置对应
}

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
    //地图房间随机种子
    private System.Random mapRng;
    //地图敌人随机种子(仅提供种子不提供生成)
    //OPTIMIZE: 现在的随机敌人生成策略: 一次生成静态种子 + 基于其他条件动态生成
    //这样能够实现动态的敌人生成, 避免房间和敌人绑定
    private System.Random enemyRng;

    private System.Random bossRng;

    //同时准备一个敌人生成器,将敌人提前保存在房间中(为了确定性生成)
    //private EnemyGroupGenerator enemyGroupGenerator;

    public MapGenerator(MapData mapData)
    {
        baseData = mapData;
        //以后使用"Map"索引随机流;
        mapRng = GameManager.Instance.RogueController.GetStream("Map");

        enemyRng = GameManager.Instance.RogueController.GetStream("Enemy");

        bossRng = GameManager.Instance.RogueController.GetStream("Boss");

        //enemyGroupGenerator = new();
    }

    /// <summary>
    /// 生成基本的随机地图,先以一维数组形式返回
    /// //TODO: 目前地图的形状是固定的,所以生成的部分有随机有预设 
    /// //OPTIMIZE: 使用了洗牌算法
    /// //NOTE: 这个函数产生了静态的地图后,后续修改逻辑需要在动态的MapState中完成
    /// </summary>
    /// <param name="levelIndex">关卡索引,用于从数据中读取</param>
    /// <param name="enemyRoomIdx">额外返回的敌人房位置索引,用于敌人房覆盖功能</param>
    /// <returns></returns>
    public MapSnapshot GenerateLevel(int levelIndex)
    {
        //存储的重要信息,在该类销毁时返回给上层的 MapState
        MapSnapshot mapSnapshot = new MapSnapshot();

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

        Shuffle(indices, mapRng);

        // 分配房间
        int cursor = 0; //指向当前分配的房间索引
        Assign(grids, indices, ref cursor, cnt.shopRoomNumbers,  GridType.Shop);
        Assign(grids, indices, ref cursor, cnt.restRoomNumbers,  GridType.Rest);
        Assign(grids, indices, ref cursor, cnt.eventRoomNumbers, GridType.Event);
        // Assign(grids, indices, ref cursor, cnt.bossRoomNumbers,  GridType.Boss);
        // auto Assign Eneny Room

        // 给战斗房间生成对应敌人
        List<(int, MapGrid)> bossGridList = new();
        for (int i = 0; i < totalGrids; i++)
        {
            if (grids[i].gridType != GridType.Enemy)
                continue;

            //不再静态生成敌人,而是根据种子和难度在进入房间时动态生成
            //grids[i].roomEnemies = enemyGroupGenerator.GetEnemyGroup(Config.Instance.difficultScore);
            
            grids[i].enemySeed = enemyRng.Next(); //为每个敌人房生成一个随机种子,后续根据难度生成敌人

            // 同时生成对应的boss代替信息
            var bossGrid = new MapGrid
            {
                gridIndex = i,
                gridType = GridType.Boss,
                nextDirection = grids[i].nextDirection, // 需要拷贝当前敌人信息
                enemySeed = bossRng.Next() //现在boss也只保留种子
                //roomEnemies = enemyGroupGenerator.GetBossGroup()
            };

            bossGridList.Add((i, bossGrid));

            // 现在敌人房不再直接生成而是随后替换
            //else if (grids[i].gridType == GridType.Boss)
            //{
            //    //非Enemy级别敌人无需组装
            //    grids[i].roomEnemies = enemyGroupGenerator.GetBossGroup();
            //}
        }

        mapSnapshot.Grids = grids;  
        mapSnapshot.BossGridBuffer = bossGridList;

        return mapSnapshot;       
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
