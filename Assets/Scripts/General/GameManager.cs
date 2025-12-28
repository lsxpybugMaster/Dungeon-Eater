using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    MAP, BATTLE,
}

public enum GameState
{
    Exploring,      //关卡选择
    Battle,         //战斗
    BattleVictory,  //胜利结算 ==> 禁用玩家战斗
    Shopping,       //进入商店界面 ==> 禁用地图交互
    Resting,
}

// GameManager 负责生命周期管理（初始化、场景切换、销毁）。
/// <summary>
/// 核心系统,跨场景而存在
//IDEA: 尽可能只管理引用及初始化,不要涉及具体逻辑
/// </summary>
public class GameManager : PersistentSingleton<GameManager>
{
    //生命周期状态,防止空指针
    public enum GameManagerPhase
    {
        None,
        Initializing,
        Ready
    }
    public GameManagerPhase Phase { get; private set; } = GameManagerPhase.None;

    [field: SerializeField] public GameState GameState { get; private set; }


    // 外部数据引用
    [SerializeField] private PersistUIController persistUIControllerPrefab;

    //STEP: 保存持久化数据 【注意】纯C#类需要实例化再用
    public HeroState HeroState { get; private set; }
    public MapState MapState { get; private set; }
    public EnemyPool EnemyPool { get; private set; }

    public LevelProgress LevelProgress { get; private set; }

    //STEP:保存功能模块(纯C#类)
    public PlayerDeckController PlayerDeckController { get; private set; }
    /// <summary>
    /// 全局随机数生成器,支持配发随机流
    /// </summary>
    public RNGSystem RogueController { get; private set; } 

    //STEP: 保存跨场景Mono实例
    public TopUI GlobalUI { get; private set; }
    public PersistUIController PersistUIController { get; private set; }

    // 初始化事件,GameManager初始化完毕后立刻通知其他脚本执行
    // public static event Action OnGameManagerInitialized;

    // 重要事件,在游戏状态改变时进行通信
    public static event Action<GameState> OnGameStateChanged;

    protected override void Awake()
    {
        //先继承跨场景单例的Awake
        base.Awake();
    }

    //防止对象还未创建
    private void Start()
    {
        Phase = GameManagerPhase.Initializing;
        RogueController = new RNGSystem(Config.Instance.Seed);
        LevelProgress = new LevelProgress();

        //数据部分由State类自己获取
        HeroState = new HeroState();
        
        EnemyPool = new(LevelProgress.Level);
        MapState = new MapState(); 

        
        //初始化全局UI对象
        InitPersistUI(); 

        //注意初始化顺序
        PlayerDeckController = new PlayerDeckController(HeroState);

        //通知其他注册了该事件的脚本进行初始化,以此确保该脚本的执行在它们前面
        //改为使用协程直接停等
        //OnGameManagerInitialized?.Invoke();

        //EnterNewLevel(1);

        Phase = GameManagerPhase.Ready;
    }


    //NOTE: 为了确保所有场景仅有一个GUI,目前只能创建一次GUI了,意味着不能在场景直接调试UI对象了,需要在Prefab中修改
    private void InitPersistUI()
    {
        if (PersistUIController == null)
        {
            PersistUIController = Instantiate(persistUIControllerPrefab);
        }
        //持久化绑定
        DontDestroyOnLoad(PersistUIController.gameObject);
        PersistUIController.Setup(HeroState);
    }


    /// <summary>
    /// 使用setter统一管理对游戏模式的更改,之后想修改直接修改这里
    /// </summary>
    /// <param name="gameState"></param>
    public void ChangeGameState(GameState gameState)
    {
        Debug.Log("Change game state to" +  gameState.ToString());
        GameState = gameState;

        //调用事件激活相关的监听者
        OnGameStateChanged?.Invoke(gameState);
    }


    //管理模式切换(不一定导致场景切换)
    //TODO: 提取成模式切换模块
    //进入战斗的入口
    public void ToBattleMode()
    {
        ChangeGameState(GameState.Battle);
        SceneManager.LoadScene((int)Scene.BATTLE);

        //大模式切换,通知其他
        PersistUIController.ResetUp();
    }

    public void ToRestMode()
    {
        ChangeGameState(GameState.Resting);
    }

    public void ToShopMode()
    {
        ChangeGameState(GameState.Shopping);
    }

    public void ToMapMode()
    {
        // 相同场景间切换
        if (GameState == GameState.Shopping)
        {
            ChangeGameState(GameState.Exploring);
            return;
        }
        HeroSystem.Instance?.SaveData();
        //在这里切换游戏模式:
        ChangeGameState(GameState.Exploring);
        SceneManager.LoadScene((int)Scene.MAP);

        //大模式切换,通知其他
        PersistUIController.ResetUp();
    }

    //进入新的大关卡
    public void EnterNewLevel(int level)
    {
        //LevelManager
        EnemyPool = new(level);
    }
}
