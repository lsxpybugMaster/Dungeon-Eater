using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//View
public class LevelProgressUI : TextUI<LevelProgress> 
{
    protected override void GetModel()
    {
        model = GameManager.Instance.LevelProgress;
    }

    protected override void UpdateTxt(LevelProgress model)
    {
        //txt.text = $"Level: {model.Level + 1} : Round {model.Round}";
        //string levelStr = model.Level + 1 == 1 ? "野炊森林" : "无尽的虚空";
        //txt.text = $"{levelStr} \n第 {model.Round + 1} 天";

        string levelKey = model.Level + 1 == 1
            ? "scene_1"
            : "scene_x";

        // 根据键值去寻找对应的本地化内容
        string levelStr = LocalizationManager.Instance.Get(levelKey);

        string dayStr = LocalizationManager.Instance.Get(
            "ui_day",
             model.Round + 1
        );

        txt.text = levelStr + "\n" + dayStr;
    }

    ////存储UI文本
    //private TMP_Text txt;

    //private LevelProgress model;

    //private void Awake()
    //{
    //    txt = GetComponentInChildren<TMP_Text>();
    //}

    //private void UpdateTxt(LevelProgress model)
    //{
    //    txt.text = $"Level: {model.Level} : Round {model.Round}";
    //}

    //void OnEnable()
    //{
    //    model = GameManager.Instance.LevelProgress;

    //    if (model == null)
    //        Debug.LogWarning("model == null");

    //    model.OnProgressChanged += UpdateTxt;
    //    UpdateTxt(model);
    //}

    //private void OnDisable()
    //{
    //    model.OnProgressChanged -= UpdateTxt;
    //}
}
