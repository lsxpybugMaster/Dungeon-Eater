using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: MapDiceView(V) MapDice(M) MapDicesSystem (IoC)
public class MapDice 
{
    //TODO: 目前只存取Index,之后可能存取其他反应
    //IMPORTANT: MapDice 作为数据层, 应该封装一切数据操作并只允许其他模块调用这些操作!

    public int Index { get; set;}
    
    // 骰子当前点数,点击后其移动这些距离
    public int Point { get; private set;}

    // 事件,用于其他模块捕捉数据变化并更新
    public event Action<int> OnPointChanged;

    public void SetPoint()
    { 
        Point = DiceRollUtil.D6(); 
        OnPointChanged?.Invoke(Point);
    }

    public void DecreasePoint(int pt)
    {
        Point -= pt;
        //由于数值更新逻辑,Point可能会更新至-1,这时候为了展示时合理,置回0
        if (Point < 0) Point = 0;
        OnPointChanged?.Invoke(Point);
    }


    //仅负责记录最初的位置便于初始化,后续不更新! 因为有Index就能推出真正位置。
    public Vector3 start_pos { get; set;}

    public MapDice(int index)
    {
        Index = index;
        SetPoint(); //初始化时自动确定骰子的点数
    }

}
