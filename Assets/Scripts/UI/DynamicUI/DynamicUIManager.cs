using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 注意设置 Sorting Layer, 其会截断更底层的输入
/// 处理一些共用的外部动态窗口: 卡牌删除/升级; 烹饪食物 ; 食用食物
/// </summary>
public class DynamicUIManager : Singleton<DynamicUIManager>
{
    [SerializeReference] private UpdateCardMenu UpdateCardMenu; //卡牌升级选择菜单
    [SerializeReference] private DeleteCardUI DeleteCardMenu;

    public UpdateCardMenu updateCardMenu => UpdateCardMenu;
    public DeleteCardUI deleteCardMenu => DeleteCardMenu;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
