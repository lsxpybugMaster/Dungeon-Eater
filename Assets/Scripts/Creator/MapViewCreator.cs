using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewCreator : MonoBehaviour {

    [SerializeField] private MapGridView mapGridPrefab;
    [SerializeField] private Transform mapGridParent; //���ɵĸ��ӹ���������
    private float gridGenerateInterval; //���Ƶ�ͼ��������֮��ľ���
    private float girdSize;

    private void Awake()
    {
        gridGenerateInterval = MapControlSystem.Instance.GridSize + MapControlSystem.Instance.GridInterval;
        girdSize = MapControlSystem.Instance.GridSize;
    }

    
    void Start()
    {
        /* //DISCUSS: �κι��ⲿ���õĳ�ʼ�������� Setup��Init��LoadData���������ı���������Ҫ�� Start ��ʼ����
          Ӧ�÷��ڣ��ֶ�����ʱ �� Awake() ��*/
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
            default: Debug.LogWarning($"δ֪�ķ���: {direction}"); break;
        }
    }

    //NOTE: ÿ�γ�ʼ��ʱ��Ҫ����һ��
    public void CreateMap(List<MapGrid> gridList)
    {
        Vector3 lastPos = transform.position;

        foreach (var grid in gridList)
        {
            MapGridView mygo = Instantiate(mapGridPrefab, lastPos, Quaternion.identity);
            mygo.Setup(grid.gridType);
            mygo.transform.SetParent(mapGridParent);
            mygo.transform.localScale = Vector3.one * girdSize;

            //���ݵ�ͼ�����������ж���һ����������������
            UpdateLastPos(ref lastPos, grid.nextDirection, gridGenerateInterval);
        }

    }
}
