using System;
using System.Collections.Generic;

/*
 战斗个体 Model ：存储数据和数据变化
 注意子类初始化数据时初始化生命
 */
public class Combatant
{
    public int MaxHealth { get; protected set; }
    public int CurrentHealth { get; protected set; }

    //记录状态的堆叠数量
    private Dictionary<StatusEffectType, int> statusEffects = new();

    //Change Event
    public event Action<int, int> OnHealthChanged;
    public event Action<StatusEffectType, int> OnEffectChanged;
    public event Action OnDamaged;

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

        OnDamaged?.Invoke();
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    //回复生命,不能过量回复
    public void Heal(int curePoint)
    {
        //防止过量回复
        CurrentHealth = Math.Min(CurrentHealth + curePoint, MaxHealth);
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
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
        OnEffectChanged?.Invoke(type, GetStatusEffectStacks(type));
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
        OnEffectChanged?.Invoke(type, GetStatusEffectStacks(type));
    }


    public int GetStatusEffectStacks(StatusEffectType type)
    {
        return statusEffects.TryGetValue(type, out var v) ? v : 0;
    }

    //直接清空Effect
    public void ClearStatusEffect(StatusEffectType type)
    {
        if (statusEffects.ContainsKey(type))
        {
            statusEffects[type] = 0;
        }
        OnEffectChanged?.Invoke(type, GetStatusEffectStacks(type));
    }


    //每轮结束/开始时自动结算一些Buff
    public void UpdateEffectStacks()
    {
        //结算护甲效果
        ClearStatusEffect(StatusEffectType.AMROR);
    }
}
