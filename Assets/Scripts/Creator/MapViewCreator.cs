using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewCreator : Singleton<MapViewCreator>
{
    [SerializeField] private MapGridView mapGridPrefab;
    [SerializeField] private float gridInterval; //控制地图格子生成之间的距离

    /* DISCUSS: 任何供外部调用的初始化（例如 Setup、Init、LoadData）所依赖的变量，都不要在 Start 初始化。
        应该放在：字段声明时 或 Awake() 中*/
    void Start()
    {
        
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

    public void CreateMap(List<MapGrid> gridList)
    {
        Vector3 lastPos = transform.position;

        foreach (var grid in gridList)
        {
            MapGridView mygo = Instantiate(mapGridPrefab, lastPos, Quaternion.identity);
            mygo.Setup(grid.gridType);

            //根据地图方格中数据判断下一个方格生成在哪里
            UpdateLastPos(ref lastPos, grid.nextDirection, gridInterval);
        }

    }
}
