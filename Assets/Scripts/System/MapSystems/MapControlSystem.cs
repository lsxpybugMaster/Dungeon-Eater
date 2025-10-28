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
