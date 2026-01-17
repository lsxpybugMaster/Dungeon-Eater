using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//从GameManger中提取的模块,用于管理模式切换和Scene切换
public class SceneModeManager
{
    GameManager gm;

    public SceneModeManager(GameManager gameManager)
    {
        gm = gameManager; 
    }

    //管理模式切换(不一定导致场景切换)
    //TODO: 提取成模式切换模块
    //进入战斗的入口
    public void ToBattleMode()
    {
        gm.ChangeGameState(GameState.Battle);
        SceneManager.LoadScene((int)Scene.BATTLE);

        //大模式切换,通知其他
        gm.PersistUIController.ResetUp();
    }

    public void ToRestMode()
    {
        gm.ChangeGameState(GameState.Resting);
    }

    public void ToShopMode()
    {
        gm.ChangeGameState(GameState.Shopping);
    }

    //从战斗场景返回地图场景时,判断是否需要更新大关卡
    public void ToMapMode()
    {
        //汇报给上层,其判断是否更新大关卡
        gm.JudgeLevelChange();

        HeroSystem.Instance?.SaveData();
        //在这里切换游戏模式:
        gm.ChangeGameState(GameState.Exploring);
        SceneManager.LoadScene((int)Scene.MAP);

        //大模式切换,通知其他
        gm.PersistUIController.ResetUp();
    }


}
