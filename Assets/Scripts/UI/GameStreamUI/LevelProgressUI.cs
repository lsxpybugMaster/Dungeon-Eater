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
        txt.text = $"Level: {model.Level + 1} : Round {model.Round}";
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
