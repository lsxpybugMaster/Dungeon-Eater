using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于初始化英雄
/// </summary>
public class HeroSystem : Singleton<HeroSystem>
{
    [field: SerializeField] public HeroView HeroView {  get; private set; }

    public void Setup(HeroData heroData)
    {
        HeroView.Setup(heroData);
    }
}
