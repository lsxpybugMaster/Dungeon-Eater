using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControlSystem : MonoBehaviour
{
    private bool hasSetup = false;

    private MapState mapState;

    [SerializeField] private MapViewCreator mapViewcreator;

    public MapViewCreator MapViewCreator => mapViewcreator;
   
    void Start()
    {
        //防止初次进入Battle场景时该脚本早于GameManager初始化,导致重复执行setup函数
        if (!hasSetup)
            SetupMap();
    }


    private void OnEnable()
    {
        //确保该系统晚于GameManager创建,防止获取不到持久数据
        GameManager.OnGameManagerInitialized += SetupMap;
    }

    private void OnDisable()
    {
        GameManager.OnGameManagerInitialized -= SetupMap;
    }

    private void SetupMap()
    {
        Debug.Log("初始化地图");
        mapState = GameManager.Instance.MapState;
        MapViewCreator.CreateMap(mapState.Map);
        hasSetup = true;
    }

}
