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
    public void Setup(HeroState heroState)
    {
        SetupBase(heroState.CurrentHealth, heroState.MaxHealth, heroState.HeroSprite);
    }

    public void SaveData()
    {
        //保存数据
        GameManager.Instance.HeroState.Save(CurrentHealth, MaxHealth);
    }

    //英雄除了更新CombatantView的UI,还需更新全局UI
    protected override void UpdateHealthText()
    {
        base.UpdateHealthText();
        //TODO: 以后调整这种不优雅的更新UI方式
        GameManager.Instance.GlobalUI.UpdateHeroHp(CurrentHealth, MaxHealth);
    }
}
