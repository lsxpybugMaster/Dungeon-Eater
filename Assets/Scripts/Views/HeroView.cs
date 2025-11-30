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
    public void Setup(HeroState heroState, HeroCombatant heroCombatant)
    {
        base.Setup(heroState.HeroSprite, heroCombatant);
    }

    public override void UpdateHealthText(int CurrentHealth, int MaxHealth)
    {
        base.UpdateHealthText(CurrentHealth, MaxHealth);

        GameManager.Instance.PersistUIController.TopUI.UpdateHeroHp(CurrentHealth, MaxHealth);
    }

    public void SaveData()
    {
        //保存数据
        GameManager.Instance.HeroState.Save(M.CurrentHealth, M.MaxHealth);
    }

}
