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

public enum GameState
{
    Exploring,      //关卡选择
    Battle,         //战斗
    BattleVictory,  //胜利结算 ==> 禁用玩家战斗
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

    public GameState GameState { get; private set; }

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


    /// <summary>
    /// 使用setter统一管理对游戏模式的更改,之后想修改直接修改这里
    /// </summary>
    /// <param name="gameState"></param>
    public void ChangeGameState(GameState gameState)
    {
        Debug.Log("Change game state to" +  gameState.ToString());
        GameState = gameState;
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
