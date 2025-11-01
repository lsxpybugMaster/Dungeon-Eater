using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ͼ����������,�����ʼ������
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
        //�������Ӳ����¼�
        int idx = 0;
        foreach (var mapDice in mapDiceList)
        {
            Debug.Log("��������");
            MapDiceView mygo = Instantiate(mapDicePrefab);
            mygo.Index = mapDiceList[idx++].Index;
            mygo.transform.position = transform.position + new Vector3(idx, 0, 0);
            //STEP: �����¼���IoC
            mygo.OnDiceClicked += HandleDiceClicked;
        }
    }

    private void HandleDiceClicked(MapDiceView mapDiceView)
    {
        Debug.Log($"���������, ����idΪ: {mapDiceView.Index}");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
