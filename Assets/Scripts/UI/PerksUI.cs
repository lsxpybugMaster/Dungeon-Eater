using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//管理PerkUI预制体
public class PerksUI : MonoBehaviour
{
    [SerializeField] private PerkUI perkUIPrefab;

    private readonly List<PerkUI> perkUIs = new();

    public void AddPerkUI(Perk perk)
    {
        PerkUI perkUI = Instantiate(perkUIPrefab, transform);
        perkUI.Setup(perk);
        perkUIs.Add(perkUI);
    }

    public void RemovePerkUI(Perk perk)
    {
        //从List中找到并删除
        PerkUI perkUI = perkUIs.Where(pui => pui.Perk == perk).FirstOrDefault();
        if (perkUI != null)
        {
            perkUIs.Remove(perkUI);
            Destroy(perkUI.gameObject);
        }
    }
}
