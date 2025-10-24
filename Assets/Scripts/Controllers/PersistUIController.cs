﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//OPTIMIZE: 现在所有持久化UI由其统一管理, GlobalUI降级为普通类
/// <summary>
/// 
/// </summary>
public class PersistUIController : MonoBehaviour
{
    //UI引用
    [SerializeField] private DeckUI deckUI;
    [SerializeField] private TopUI topUI;

    public DeckUI DeckUI => deckUI;
    public TopUI TopUI => topUI;

    /// <summary>
    /// 初始化基本信息
    /// </summary>
    //NOTE: 这部分由GameManager调用 
    public void Setup(HeroState heroState, PlayerDeckController playerDeckController)
    {

        //topUI初始化时需要绑定按钮
        topUI.Setup(heroState, playerDeckController, () =>
        {
            deckUI.MoveUI();
        });
        Debug.Log("绑定按钮");

        deckUI.Setup();
    }
   
}
