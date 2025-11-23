using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 理解为Entity脚本,控制所有游戏角色的逻辑(如生命)
/// </summary>
public class CombatantView : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    //对象图片
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private StatusEffectsUI statusEffectsUI;

    public int MaxHealth {  get; private set; }
    public int CurrentHealth { get; private set; }

    //记录状态的堆叠数量
    private Dictionary<StatusEffectType, int> statusEffects = new();

    //IDEA: 数据的改变事件,一般是由UI响应的
    public event Action<int, int> OnHealthChanged;


    protected void SetupBase(int health, int maxhealth, Sprite image)
    {
        MaxHealth = maxhealth;
        CurrentHealth = health;
        spriteRenderer.sprite = image;
        UpdateHealthText();
    }

    protected virtual void UpdateHealthText()
    {
        healthText.text = "HP: " + CurrentHealth;
        //IDEA: 引发事件
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    /// <summary>
    /// 供damageSystem调用
    /// </summary>
    /// <param name="damageAmount"></param>
    public void Damage(int damageAmount)
    {
        //依据护甲计算实际伤害
        int remainingDamage = damageAmount;
        int currentArmor = GetStatusEffectStacks(StatusEffectType.AMROR);
        if (currentArmor > 0)
        {
            if (currentArmor >= damageAmount)
            {
                RemoveStatusEffect(StatusEffectType.AMROR, remainingDamage);
                remainingDamage = 0;
            }
            else if (currentArmor < damageAmount)
            {
                RemoveStatusEffect(StatusEffectType.AMROR, currentArmor);
                remainingDamage -= currentArmor;
            }
        }
        if (remainingDamage > 0)
        {
            CurrentHealth -= damageAmount;
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
        }

        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealthText();
    }

    //回复生命,不能过量回复
    public void Heal(int curePoint)
    {
        //防止过量回复
        CurrentHealth = Math.Min(CurrentHealth + curePoint, MaxHealth);
        UpdateHealthText();
    }

    public void AddStatusEffect(StatusEffectType type, int stackCount)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] += stackCount;
        }
        else
        {
            statusEffects.Add(type, stackCount);
        }
        statusEffectsUI.UpdateStatusEffectUI(type, GetStatusEffectStacks(type));
    }


    public void RemoveStatusEffect(StatusEffectType type, int stackCount)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] -= stackCount;
            if (statusEffects[type] <= 0)
            {
                statusEffects.Remove(type);
            }
        }
        statusEffectsUI.UpdateStatusEffectUI(type, GetStatusEffectStacks(type));
    }
    

    //直接清空Effect
    public void ClearStatusEffect(StatusEffectType type)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] = 0;
        }
        statusEffectsUI.UpdateStatusEffectUI(type, GetStatusEffectStacks(type));
    }


    public int GetStatusEffectStacks(StatusEffectType type)
    {
        if (statusEffects.ContainsKey(type)) return statusEffects[type];
        else return 0;
    }

    //每轮结束/开始时自动结算一些Buff
    public void UpdateEffectStacks()
    {
        //结算护甲效果
        ClearStatusEffect(StatusEffectType.AMROR);
    }
}
