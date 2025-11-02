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
        Debug.Log($"点击了骰子, 骰子id为: {mapDiceView.MapDice.Index}");
        int res = DiceRollUtil.D6();
        DebugUtil.Cyan($"ROLL: {res}");
        mapDiceView.MoveToTarget(res);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
