using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于管理鼠标交互,防止Hover与Up,Down冲突
public class Interactions : Singleton<Interactions>
{
    public bool PlayerIsDragging { get; set; } = false;

    /// <summary>
    /// 如果GameAction系统在执行动作,那么玩家无法交互
    /// </summary>
    /// <returns></returns>
    public bool PlayerCanInteract()
    {
        if (!ActionSystem.Instance.IsPerforming) return true;
        else return false;
    }

    /// <summary>
    /// 如果玩家在拖动卡牌，则停止鼠标进入移出的判断, 不再展示卡牌
    /// </summary>
    /// <returns></returns>

    public bool PlayerCanHover()
    {
        if (PlayerIsDragging) return false;
        else return true;
    }    


}
