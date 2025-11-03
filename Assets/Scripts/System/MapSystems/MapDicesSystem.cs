using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理地图的所有骰子,负责初始化骰子
/// </summary>
//IMPORTANT: 骰子的最顶层
public class MapDicesSystem : Singleton<MapDicesSystem>
{
    [SerializeField] private MapDiceView mapDicePrefab;    
    

    public void SetUp(List<MapDice> mapDiceList)
    {
        //IDEA: 能"注入"就不要"全局访问"  问自己: 如果我复制这个 System 到另一个项目或测试场景，它还能独立工作吗?
        //OPTIMIZE: 既然MapDicesSystem是被MapControlSystem调用的模块,那么最好使用DI将mapList注入
        if (mapDiceList.Count == 0 || mapDiceList == null)
            Debug.LogError("mapDiceList.Count == 0 or mapDiceList is null");
    
        //生成骰子并绑定事件
        int idx = 0;
        foreach (var mapDice in mapDiceList)
        {
            MapDiceView mygo = Instantiate(mapDicePrefab);
            mygo.MapDice = mapDiceList[idx++];
            //TODO: 绑定位置
            mygo.transform.position = mygo.MapDice.start_pos; //在mapViewcreator.CreateMapWithDice初始化了start_pos
            //STEP: 基于事件的IoC 通过事件绑定骰子
            mygo.OnDiceClicked += HandleDiceClicked;
        }
    }

    private void HandleDiceClicked(MapDiceView mapDiceView)
    {
        int res = DiceRollUtil.D6();
        //分析骰子的移动,形成指令      
        int id = mapDiceView.MapDice.Index;
        //TODO: 这里必须从全局获取Map吗？
        var map = GameManager.Instance.MapState.Map;

        //模拟移动,并未真实移动
        MoveDataOnMap(id, res, map, out string moveCommand, out int newId);
        //给下层传入命令,具体的移动MapDicesSystem无需了解
        mapDiceView.MoveToTarget(moveCommand);
    }


    /// <summary>
    /// 模拟移动 
    /// 传入当前id,以及移动的步数,返回移动所需的路径(字符串)和新的id
    /// </summary>
    /// <returns>返回新的id</returns>
    private void MoveDataOnMap(int idx, int step, List<MapGrid> map, out string moveStr, out int newIdx)
    {
        moveStr = "";

        int mapSize = map.Count;

        while (step-- > 0)
        {
            //先从id获取下一次位置,再去更新id
            char direct = map[idx].nextDirection;
            moveStr += direct;

            idx = (idx + 1) % mapSize;
        }

        newIdx = idx;
    }
}
