using ActionSystemTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于初始化英雄
/// </summary>
public class HeroSystem : Singleton<HeroSystem>
{
    [field: SerializeField] public HeroView HeroView {  get; private set; }

    private void OnEnable()
    {
        //注册Reaction,指明在对什么行动做出反应
      
    }

    private void OnDisable()
    {
       
    }

    public void Setup(HeroState heroState)
    {
        HeroCombatant combatant = new(heroState);
        HeroView.Setup(heroState, combatant);
    }


    /// <summary>
    /// 对应Setup,将数据返回
    /// </summary>
    public void SaveData()
    {
        HeroView.SaveData();
    }

    //TODO: 移除逻辑

    //Reactions
}
