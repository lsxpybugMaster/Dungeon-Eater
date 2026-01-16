using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//专门用于管理Model-View形式中需要渲染文字并依据Model更新的View
public class TextUI<TModel> : MonoBehaviour where TModel : IModelForUI<TModel>
{
    //存储UI文本
    protected TMP_Text txt;

    protected TModel model;

    protected void Awake()
    {
        txt = GetComponentInChildren<TMP_Text>();
    }

    protected virtual void UpdateTxt(TModel model)
    {
        txt.text = $"model:{model} is not implemented";
    }

    protected virtual void GetModel()
    {
        if (model == null)
        {
            Debug.LogWarning("未在子类获取Model");
        }
    }

    protected void OnEnable()
    {
        GetModel();
        // model = GameManager.Instance.ModelName;

        model.OnModelChanged += UpdateTxt;
        UpdateTxt(model);
    }

    protected void OnDisable()
    {
        model.OnModelChanged -= UpdateTxt;
    }
}
