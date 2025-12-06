using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsUI : MonoBehaviour
{
    [SerializeField] private StatusEffectUI statusEffectUIPrefab;

    //UI对象字典
    private Dictionary<StatusEffectType, StatusEffectUI> statusEffectUIs = new();

    /// <summary>
    /// 根据状态层数及时更新UI图标
    /// </summary>
    public void UpdateStatusEffectUI(StatusEffectType statusEffectType, int stackCount)
    {
        if (stackCount == 0)
        {
            if (statusEffectUIs.ContainsKey(statusEffectType))
            {
                StatusEffectUI statusEffectUI = statusEffectUIs[statusEffectType];
                statusEffectUIs.Remove(statusEffectType);
                Destroy(statusEffectUI.gameObject);
            }
        }
        else
        {
            if (!statusEffectUIs.ContainsKey(statusEffectType))
            {
                StatusEffectUI statusEffectUI = Instantiate(statusEffectUIPrefab, transform);
                statusEffectUIs.Add(statusEffectType, statusEffectUI);
            }
            Sprite sprite = GetSpriteByType(statusEffectType);
            statusEffectUIs[statusEffectType].Set(sprite, stackCount);
        }
    }

    //根据状态类型返回对应sprite
    private Sprite GetSpriteByType(StatusEffectType statusEffectType)
    {
        //return statusEffectType switch
        //{
        //    StatusEffectType.AMROR => armorSprite,
        //    StatusEffectType.BURN => burnSprite,
        //    _ => null,
        //};
        return StatusEffectDataBase.GetEffect(statusEffectType).sprite;
    }
}
