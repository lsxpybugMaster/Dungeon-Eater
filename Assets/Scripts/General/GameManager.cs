using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum Scene
{
    MAP, BATTLE,
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
    // 外部数据引用
    [SerializeField] private HeroData heroData;

    // 属性
    public GameState GameState { get; private set; }

    // 保存持久化数据 【注意】纯C#类需要实例化再用
    public HeroState HeroState { get; private set; } = new();

    // 初始化事件,GameManager初始化完毕后立刻通知其他脚本执行
    public static event Action OnGameManagerInitialized;

    protected override void Awake()
    {
        //先继承跨场景单例的Awake
        base.Awake();
    }

    //防止对象还未创建
    private void Start()
    {
        HeroState.Init(heroData);
        GlobalUI.Instance.Setup(HeroState);

        //通知其他注册了该事件的脚本进行初始化,以此确保该脚本的执行在它们前面
        Debug.Log("Invoke");
        OnGameManagerInitialized?.Invoke();
    }

    private void Update()
    {

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


    //管理场景切换

    public void ToBattleScene()
    {
        ChangeGameState(GameState.Battle);
        SceneManager.LoadScene((int)Scene.BATTLE);    
    }


    public void ToMapScene()
    {
        HeroSystem.Instance?.SaveData();
        //在这里切换游戏模式:
        ChangeGameState(GameState.Exploring);
        SceneManager.LoadScene((int)Scene.MAP);
    }
}
