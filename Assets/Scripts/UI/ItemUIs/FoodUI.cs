using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodUI : ItemUI<FoodData>
{
    //继承ItemUI的子类只需声明对应需要的属性然后在Refresh方法中实现数据与UI的绑定即可
    protected override void Refresh(FoodData data)
    {
        image.sprite = data.Image;
    }
}
