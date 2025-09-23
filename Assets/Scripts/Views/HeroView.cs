using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroView : CombatantView
{
    /// <summary>
    /// 需要HeroSystem执行此方法
    /// </summary>
    /// <param name="heroData"></param>
    public void Setup(HeroData heroData)
    {
        SetupBase(heroData.Health, heroData.Image);
    }
}
