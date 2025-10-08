using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum Scene
{
    BATTLE
}
/// <summary>
/// 核心系统,跨场景而存在
/// </summary>
public class GameManager : PersistentSingleton<GameManager>
{
    private int state = 0;

    public static event Action OnReturnToMenu;

    // 外部数据引用
    [SerializeField] private HeroData heroData;

    // 保存持久化数据 【注意】纯C#类需要实例化再用
    public HeroState HeroState { get; private set; } = new();

    protected override void Awake()
    {
        //先继承跨场景单例的Awake
        base.Awake();

        HeroState.Init(heroData);
    }

    private void Update()
    {
        if (state == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            ReturnToMain();  
        }
    }

    public void ToBattle()
    {
        SceneManager.LoadScene((int)Scene.BATTLE);
        state = 1;
    }

    public void ReturnToMain()
    {
        //初始化
        OnReturnToMenu?.Invoke();
        SceneManager.LoadScene(1);
        state = 0;
    }


}
