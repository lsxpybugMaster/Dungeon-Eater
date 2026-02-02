using System;

//记录当前的关卡流程信息
//注意LevelProgress早于MapState初始化
public class LevelProgress : IModelForUI<LevelProgress>
{
    public int MaxLevel { get; set; }

    //当前关卡, 从 0 开始
    public int Level { get; private set; }
  

    //当前轮次, 即该关卡走了多少房间
    public int Round { get; private set; }

    //传入level, Round 
    //public event Action<LevelProgress> OnProgressChanged;

    public LevelProgress()
    {
        Level = 0;
        Round = 0;
    }

    //这个专门与 UI 连接, 其他的额外
    public event Action<LevelProgress> OnModelChanged;

    public event Action<LevelProgress> OnRoundIncreased;

    //判断是否最后一大关也通关,由此判断胜利条件
    public bool IsFinalLevel()
    {
        //因为level从0开始
        return Level == MaxLevel - 1;
    }

    /// <summary>
    /// 在这里分发level增加的信息
    /// </summary>
    public void IncreaseLevel()
    {
        Level++;
        Round = 0;
        // 此时可能UI还为准备
        // OnModelChanged.Invoke(this);
    }

    /// <summary>
    /// 在这里分发round增加的信息
    /// </summary>
    public void IncreaseRound()
    {
        Round++;
        OnModelChanged.Invoke(this);
        OnRoundIncreased?.Invoke(this);
    }
    
}
