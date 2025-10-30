using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ʼ����ͼ,�Լ��洢һЩ��ͼ������Ϣ(�ڱ༭���б༭��)
/// </summary>
public class MapControlSystem : Singleton<MapControlSystem>
{
    private bool hasSetup = false;

    private MapState mapState;

    [SerializeField] private MapViewCreator mapViewcreator;

    [Header("��ͼ����ر༭����������")]
    [SerializeField] private float gridInterval;
    [SerializeField] private float gridSize;

    public MapViewCreator MapViewCreator => mapViewcreator;

    //��װ��Ҫ��¶���ֶ�Ϊ����
    public float GridInterval => gridInterval;
    public float GridSize => gridSize;
   
    void Start()
    {
        //��ֹ���ν���Battle����ʱ�ýű�����GameManager��ʼ��,�����ظ�ִ��setup����
        if (!hasSetup)
            SetupMap();
    }


    private void OnEnable()
    {
        //ȷ����ϵͳ����GameManager����,��ֹ��ȡ�����־�����
        GameManager.OnGameManagerInitialized += SetupMap;
    }

    private void OnDisable()
    {
        GameManager.OnGameManagerInitialized -= SetupMap;
    }

    private void SetupMap()
    {
        Debug.Log("��ʼ����ͼ");
        mapState = GameManager.Instance.MapState;
        MapViewCreator.CreateMap(mapState.Map);
        hasSetup = true;
    }

}
