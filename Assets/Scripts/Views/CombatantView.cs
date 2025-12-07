using DG.Tweening;
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

    public Combatant Combatant { get; set; }

    //FIXME: 实际使用时需要强制转换,不太方便,不使用则需要每个子类声明一次,也不方便
    public Combatant M => Combatant;

    /*
        View初始化逻辑
     */
    protected virtual void Setup(Sprite image, Combatant combatant)
    {
        Combatant = combatant;
        //TODO: 临时绑定
        Combatant.view = this;

        BindEvents();

        spriteRenderer.sprite = image;

        UpdateHealthText(M.CurrentHealth, M.MaxHealth);
    }

    protected virtual CombatantView ReturnThis()
    {
        return this;
    }

    protected virtual void BindEvents()
    { 
        Combatant.OnHealthChanged += UpdateHealthText;
        Combatant.OnEffectChanged += UpdateEffect;
        Combatant.OnDamaged += Shake;
    }

    protected virtual void UnBindEvents()
    {

        Combatant.OnHealthChanged -= UpdateHealthText;
        Combatant.OnEffectChanged -= UpdateEffect;
        Combatant.OnDamaged -= Shake;
    }


    private void OnDestroy()
    {
        UnBindEvents();
    }


    //protected virtual void UpdateHealthText()
    //{
    //    healthText.text = $"HP: {CurrentHealth}";
    //}

    public virtual void UpdateHealthText(int CurrentHealth, int MaxHealth)
    {
        healthText.text = $"HP: {CurrentHealth}";
    }

    public void UpdateEffect(StatusEffectType type, int stacks)
    {
        statusEffectsUI.UpdateStatusEffectUI(type, stacks);
    }

    public void Shake()
    {
        transform.DOShakePosition(0.2f, 0.5f);
    }
}
