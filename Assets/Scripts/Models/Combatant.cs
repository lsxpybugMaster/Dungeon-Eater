using System;
using System.Collections.Generic;
using UnityEngine;

/*
 战斗个体 Model ：存储数据和数据变化
 注意子类初始化数据时初始化生命
 */
public class Combatant
{
    //参照CombatantData
    public int MaxHealth { get; protected set; }
    public int CurrentHealth { get; protected set; }
    public int Proficiency { get; protected set; }
    public int ProficiencyBuff => GetStatusEffectStacks(StatusEffectType.PROFICIENCY);
    public int Flexbility { get; protected set; }
    public int FlexbilityBuff => GetStatusEffectStacks(StatusEffectType.FLEXBILITY);

    //记录状态的堆叠数量
    private Dictionary<StatusEffectType, int> statusEffects = new();

    //IMPORTANT: Combantant使用事件与CombantantView交流
    //Change Event
    public event Action<int, int> OnHealthChanged;
    public event Action<StatusEffectType, int> OnEffectChanged;
    public event Action OnDamaged;
 
    //TODO: 注意其破坏了MVC架构! 使用特殊的名字,代表要谨慎使用该变量!!
    public CombatantView __view__ { get; set; }

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

        //真正处理生命的损失
        DecreaseHealth(remainingDamage);

        OnDamaged?.Invoke();
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    private void DecreaseHealth(int amount)
    {
        if (amount < 0)
            return;

        CurrentHealth -= amount;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }

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

    //快速获取一些状态,用于判断
    public bool HasStatus(StatusEffectType type)
    {
        return GetStatusEffectStacks(type) > 0;
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
    public void EffectsOnTurnStart()
    {
        List<StatusEffectType> keysSnapshot = new(statusEffects.Keys);

        foreach (var type in keysSnapshot)
        {
            StatusEffectDataBase.GetEffect(type).OnTurnStart(this);
        }
    }


    public void EffectsOnTurnEnd()
    {
        //NOTE: 在 foreach 遍历字典时，不允许对字典进行任何修改操作
        //所以需要额外存储key列表
        List<StatusEffectType> keysSnapshot = new(statusEffects.Keys);
        
        foreach (var type in keysSnapshot)
        {
            StatusEffectDataBase.GetEffect(type).OnTurnEnd(this);
        }
    }
    
   
}
