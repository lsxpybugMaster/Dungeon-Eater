using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//实现该接口的类会作为道具, 获取时执行对应功能
public interface IFood
{
    public void OnPickup();

    public void OnReMove();
}
