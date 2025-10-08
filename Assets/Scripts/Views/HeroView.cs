using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroView : CombatantView
{
    /// <summary>
    /// 需要HeroSystem执行此方法
    /// </summary>
    /// <param name="heroState"></param>
    // 可持久化部分使用heroState,其余正常
    public void Setup(HeroState heroState, HeroData heroData)
    {
        SetupBase(heroState.CurrentHealth, heroState.MaxHealth, heroData.Image);
    }

    public void SaveData()
    {
        //保存数据
        GameManager.Instance.HeroState.Save(CurrentHealth, MaxHealth);
    }
}
