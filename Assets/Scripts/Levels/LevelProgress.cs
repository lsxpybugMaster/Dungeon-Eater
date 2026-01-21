using System;

//记录当前的关卡流程信息
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

    public event Action<LevelProgress> OnModelChanged;

    //判断是否最后一大关也通关,由此判断胜利条件
    public bool IsFinalLevel()
    {
        //因为level从0开始
        return Level == MaxLevel - 1;
    }

    public void IncreaseLevel()
    {
        Level++;
        Round = 0;
        // 此时可能UI还为准备
        // OnModelChanged.Invoke(this);
    }

    public void IncreaseRound()
    {
        Round++;
        OnModelChanged.Invoke(this);
    }
    
}
