using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewCreator : Singleton<MapViewCreator>
{
    public MapGridView mapGridPrefab;
    private Vector3 v;
    
    void Start()
    {
        v = new Vector3(1,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateMap(List<MapGrid> gridList)
    {
        int pos = 0;
        foreach (var grid in gridList)
        {
            MapGridView mygo = Instantiate(mapGridPrefab, transform.position + v * pos , Quaternion.identity);
            mygo.Setup(grid.gridType);
            pos+=2;
        }

    }
}
