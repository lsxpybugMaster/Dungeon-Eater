using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//记录当前的关卡流程信息
public class LevelProgress
{
    //当前关卡, 从 1 开始
    public int Level { get; private set; }

    //当前轮次, 即该关卡走了多少房间
    public int Round { get; private set; }

    public LevelProgress()
    {
        Level = 1;
        Round = 0;
    }

    public void IncreaseLevel()
    {
        Level++;
    }
    public void IncreaseRound()
    {
        Round++;
    }
    
}
