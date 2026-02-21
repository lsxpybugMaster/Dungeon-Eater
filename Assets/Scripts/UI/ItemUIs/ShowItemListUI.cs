using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 展示一系列 UI 的
/// </summary>
public class ShowItemListUI<TData> : MonoBehaviour
{
    [Header("生成的UI预制体")]
    [SerializeField] private GameObject itemUIPrefab;
    [Header("UI生成在哪个父节点下")]
    [SerializeField] private GameObject itemUIRoot; //UI放置位置

    //调试的功能
    public List<TData> d;

    //保存所有itemUI; 以便于后续管理
    public List<ItemUI<TData>> itemUIs { get; set; } = new();

    //由上层调用, 进行具体itemUIs的展示
    public virtual void Show(List<TData> datas, bool isGroup)
    {
        itemUIs.Clear();

        datas = d;

        //清空之前的
        for (int i = itemUIRoot.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(itemUIRoot.transform.GetChild(i).gameObject);
        }

        //生成新的
        for (int i = 0; i < datas.Count; i++)
        {
            var data = datas[i];
            int id = isGroup ? i : -1;
            InstantiateItemUIPrefab(data, idx: id);
        }
    }

    /// <summary>
    /// 封装一个itemUI的生成方法
    /// </summary>
    /// <param name="data"></param>
    /// <param name="idx"></param>
    private void InstantiateItemUIPrefab(TData data, int idx)
    {
        var inst = Instantiate(itemUIPrefab).GetComponent<ItemUI<TData>>();
        inst.transform.SetParent(itemUIRoot.transform);

        if (idx >= 0)
            inst?.SetupForGroup(data, idx);
        else
            inst?.Setup(data);

        itemUIs.Add(inst);
    }

    //绑定选择事件, 由顶层进行具体的调用
    public void BindOnItemSelected(Action<TData> onItemSelected)
    {
        foreach (var item in itemUIs)
        {
            item.OnItemSelected += onItemSelected;
        }
    }

    public void BindOnItemSelectedInGroup(Action<TData, int> onItemSelected)
    {
        foreach (var item in itemUIs)
        {
            item.OnItemSelectedInGroup += onItemSelected;
        }
    }
}
