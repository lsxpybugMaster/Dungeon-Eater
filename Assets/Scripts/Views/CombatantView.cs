using DG.Tweening;
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

    public int MaxHealth {  get; private set; }
    public int CurrentHealth { get; private set; }

    protected void SetupBase(int health, Sprite image)
    {
        MaxHealth = CurrentHealth = health;
        spriteRenderer.sprite = image;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = "HP: " + CurrentHealth;
    }

    /// <summary>
    /// 供damageSystem调用
    /// </summary>
    /// <param name="damageAmount"></param>
    public void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if(CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealthText();
    }
}
