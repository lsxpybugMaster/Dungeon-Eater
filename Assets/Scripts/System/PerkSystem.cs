using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//在MatchUpSystem中初始化
public class PerkSystem : Singleton<PerkSystem>
{
    private readonly List<Perk> perks = new();

    [SerializeField] private PerksUI perksUI;


    //初始化Perk信息序列
    public void Setup(List<PerkData> perkDatas)
    {
        foreach (var data in perkDatas)
        {
            AddPerk(new Perk(data));
        }
    }

    public void AddPerk(Perk perk)
    {
        perks.Add(perk);
        perksUI.AddPerkUI(perk);
        //在这里注册天赋
        perk.OnAdd();
    }

    public void RemovePerk(Perk perk)
    {
        perks.Remove(perk);
        perksUI.RemovePerkUI(perk);
        //在这里取消注册天赋
        perk.OnMove();
    }
}
