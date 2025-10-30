using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地图上的骰子,主要控制玩家的地图探索
/// </summary>
//DISCUSS: 点击骰子后触发的功能还是上报给上层计算好
//STEP: 控制反转（Inversion of Control, IoC）" 下层模块不依赖上层模块，控制流程交由外部统一管理。"
public class MapDiceView : MonoBehaviour
{
    [SerializeField] private float hover_and_rotate_speed = 60f;

    private bool isMoveHovering = false;
    //NOTE: 基于事件的控制反转IoC
    public event Action<MapDiceView> OnClicked;

    private void Update()
    {
        OnMouseHover();   
    }

    private void OnMouseHover()
    {
        if (isMoveHovering)
        {
            transform.Rotate(0, 0, hover_and_rotate_speed * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        float x = transform.position.x;
        transform.DOMoveX(x + 2, 0.5f);
    }

    //注意要求有碰撞体才能运行
    private void OnMouseEnter()
    {
        isMoveHovering = true;
    }

    private void OnMouseExit() 
    { 
        isMoveHovering = false;
    }
}
