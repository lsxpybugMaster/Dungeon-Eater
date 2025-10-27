using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapViewCreator : Singleton<MapViewCreator>
{
    [SerializeField] private MapGridView mapGridPrefab;
    [SerializeField] private float gridInterval; //���Ƶ�ͼ��������֮��ľ���

    /* DISCUSS: �κι��ⲿ���õĳ�ʼ�������� Setup��Init��LoadData���������ı���������Ҫ�� Start ��ʼ����
        Ӧ�÷��ڣ��ֶ�����ʱ �� Awake() ��*/
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
            default: Debug.LogWarning($"δ֪�ķ���: {direction}"); break;
        }
    }

    public void CreateMap(List<MapGrid> gridList)
    {
        Vector3 lastPos = transform.position;

        foreach (var grid in gridList)
        {
            MapGridView mygo = Instantiate(mapGridPrefab, lastPos, Quaternion.identity);
            mygo.Setup(grid.gridType);

            //���ݵ�ͼ�����������ж���һ����������������
            UpdateLastPos(ref lastPos, grid.nextDirection, gridInterval);
        }

    }
}
