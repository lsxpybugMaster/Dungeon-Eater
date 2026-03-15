using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsUI : MonoBehaviour
{
    [SerializeField] private StatusEffectUI statusEffectUIPrefab;

    [SerializeField] private StatusUI statusUIPrefab;

    //UI对象字典
    //private Dictionary<StatusEffectType, StatusEffectUI> statusEffectUIs = new();

    private Dictionary<StatusEffectType, StatusUI> statusUIList = new();

    /// <summary>
    /// 根据状态层数及时更新UI图标
    /// </summary>
    public void UpdateStatusEffectUI(StatusEffectType statusEffectType, int stackCount)
    {
        if (stackCount == 0)
        {
            if (statusUIList.ContainsKey(statusEffectType))
            {
                StatusUI statusEffectUI = statusUIList[statusEffectType];
                statusUIList.Remove(statusEffectType);
                Destroy(statusEffectUI.gameObject);
            }
        }
        else
        {
            if (!statusUIList.ContainsKey(statusEffectType))
            {
                StatusUI statusEffectUI = Instantiate(statusUIPrefab, transform);
                statusUIList.Add(statusEffectType, statusEffectUI);
            }

            //Sprite sprite = GetSpriteByType(statusEffectType);
            //statusUIList[statusEffectType].Set(sprite, stackCount);
            var ui = statusUIList[statusEffectType];
            ui.SetStackCount(stackCount);
            ui.Setup(StatusDatabase.GetStatusData(statusEffectType));
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
