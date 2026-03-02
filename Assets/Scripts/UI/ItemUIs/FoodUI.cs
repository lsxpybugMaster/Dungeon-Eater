using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodUI : ItemUI<FoodData>, ITooltipProvider
{
    //继承ItemUI的子类只需声明对应需要的属性然后在Refresh方法中实现数据与UI的绑定即可
    protected override void Refresh(FoodData data)
    {
        image.sprite = data.Image;
    }

    protected override void MouseEnterEffect()
    {
        base.MouseEnterEffect();

        TooltipManager.Instance?.Show(GetTooltipData());
    }

    protected override void MouseExitEffect()
    {
        base.MouseExitEffect();

        TooltipManager.Instance?.Hide();
    }

    public TooltipData GetTooltipData()
    {
        return new TooltipData(
            Data.name,
            "show description"
        );
    }
}
