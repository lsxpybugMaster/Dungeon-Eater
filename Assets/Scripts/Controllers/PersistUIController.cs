using System.Collections;
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
    

    private void Start()
    {

    }

    /// <summary>
    /// 初始化基本信息
    /// </summary>
    public void Setup(HeroState heroState, PlayerDeckController playerDeckController)
    {
        DebugUtil.Cyan("HELLO");   
    }
   
}
