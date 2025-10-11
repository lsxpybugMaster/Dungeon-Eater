using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// �糡����ʾ��ȫ��UI
/// </summary>
public class GlobalUI : PersistentSingleton<GlobalUI>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text heroHpTMP;
    

    private void Start()
    {

    }

    /// <summary>
    /// ��ʼ��������Ϣ
    /// </summary>
    public void Setup(HeroState heroState)
    {
        UpdateHeroHp(heroState.CurrentHealth, heroState.MaxHealth);
    }


    public void UpdateHeroHp(int hpAmount, int maxHpAmount)
    {
        print($"{hpAmount} , {maxHpAmount} ");
        heroHpTMP.text = hpAmount.ToString() + "/" + maxHpAmount.ToString();
    }
}