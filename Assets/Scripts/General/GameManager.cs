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

    private void Update()
    {
        if (state == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
            state = 0;
        }
    }

    public void ToBattle()
    {
        SceneManager.LoadScene((int)Scene.BATTLE);
        state = 1;
    }


}
