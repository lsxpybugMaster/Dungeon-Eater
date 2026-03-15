using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBackGroundSystem : MonoBehaviour
{
    [SerializeField] private Sprite[] backgroundSprites;

    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        // 获取当前游戏对象上的SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 更换背景的方法
    /// </summary>
    public void ChangeBackground(bool isBoss = false)
    {
        int n = backgroundSprites.Length;
        int id = isBoss? n - 1 : Random.Range(0, n - 1);

        Sprite selectedSprite = backgroundSprites[id];

        spriteRenderer.sprite = selectedSprite;
    }

}
