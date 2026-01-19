using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 含文本和按钮的对话框式UI基类,便于以后的代码复用
/// </summary>
public abstract class TextBtnUI : MonoBehaviour
{
    [SerializeField] protected List<Button> btnList;

    protected abstract void BindBtnInbtnList(List<Button> btnList);

    private void Awake()
    {
        BindBtnInbtnList(btnList);
    }

    protected void BindBtn(int index, Action doWhenClicked)
    {
        var btn = btnList[index];
        btn.onClick.RemoveAllListeners(); // 防止重复注册,之前踩过坑
        btn.onClick.AddListener(() => { doWhenClicked(); });
    }
}
