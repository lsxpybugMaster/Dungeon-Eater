using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewCreator : MonoBehaviour {

    [SerializeField] private MapGridView mapGridPrefab;
    [SerializeField] private Transform mapGridParent; //生成的格子挂载在哪里
    private float gridGenerateInterval; //控制地图格子生成之间的距离
    private float girdSize;

    [SerializeField] private Transform mapStartPoint; //地图起始点

    private void Awake()
    {
        gridGenerateInterval = MapControlSystem.Instance.GridSize + MapControlSystem.Instance.GridInterval;
        girdSize = MapControlSystem.Instance.GridSize;

        mapStartPoint = transform;
    }

    
    void Start()
    {
        /* //DISCUSS: 任何供外部调用的初始化（例如 Setup、Init、LoadData）所依赖的变量，都不要在 Start 初始化。
          应该放在：字段声明时 或 Awake() 中*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateLastPos(ref Vector3 lastPos, char direction, float interval)
    {
        switch (direction)
        {
            case 'U': lastPos.y += interval; break;
            case 'D': lastPos.y -= interval; break;
            case 'L': lastPos.x -= interval; break;
            case 'R': lastPos.x += interval; break;
            default: Debug.LogWarning($"未知的方向: {direction}"); break;
        }
    }

    //NOTE: 每次初始化时都要生成一次, 现在还需控制骰子生成位置
    public void CreateMapWithDice(List<MapGrid> gridList, List<MapDice> mapDices)
    {
        Vector3 lastPos = mapStartPoint.position;

        Dictionary<int, MapDice> diceDict = new Dictionary<int, MapDice>();
        foreach (var dice in mapDices)
        {
            diceDict[dice.Index] = dice;
        }

        for (int i = 0; i < gridList.Count; i++)
        {
            var grid = gridList[i];

            MapGridView mygo = Instantiate(mapGridPrefab, lastPos, Quaternion.identity);
            mygo.Setup(grid.gridType);
            mygo.transform.SetParent(mapGridParent);
            mygo.transform.localScale = Vector3.one * girdSize;

            //如果当前格子与dice中某id一致,那么该位置会被写入dice供后续生成绑定
            if (diceDict.TryGetValue(i, out MapDice dice))
            {
                dice.start_pos = lastPos;
            }
              
            //根据地图方格中数据判断下一个方格生成在哪里
            UpdateLastPos(ref lastPos, grid.nextDirection, gridGenerateInterval);
        }
        
    }
}
