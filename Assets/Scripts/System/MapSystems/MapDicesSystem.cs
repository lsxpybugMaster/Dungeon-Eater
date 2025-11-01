using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理地图的所有骰子,负责初始化骰子
/// </summary>
public class MapDicesSystem : Singleton<MapDicesSystem>
{
    [SerializeField] private MapDiceView mapDicePrefab;    
    

    public void SetUp()
    {
        Debug.Log("Begin: To Get MapDiceList");
        var mapDiceList = GameManager.Instance.MapState.MapDiceList;
        if (mapDiceList.Count == 0)
            Debug.LogError("mapDiceList.Count == 0");
        Debug.Log($"GameManager.Instance.MapState.MapDiceList: {GameManager.Instance.MapState.MapDiceList.Count}");
        //生成骰子并绑定事件
        int idx = 0;
        foreach (var mapDice in mapDiceList)
        {
            Debug.Log("生成骰子");
            MapDiceView mygo = Instantiate(mapDicePrefab);
            mygo.Index = mapDiceList[idx++].Index;
            mygo.transform.position = transform.position + new Vector3(idx, 0, 0);
            //STEP: 基于事件的IoC
            mygo.OnDiceClicked += HandleDiceClicked;
        }
    }

    private void HandleDiceClicked(MapDiceView mapDiceView)
    {
        Debug.Log($"点击了骰子, 骰子id为: {mapDiceView.Index}");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
