using System;

//记录当前的关卡流程信息
public class LevelProgress : IModelForUI<LevelProgress>
{
    //当前关卡, 从 1 开始
    public int Level { get; private set; }

    //当前轮次, 即该关卡走了多少房间
    public int Round { get; private set; }

    //传入level, Round 
    //public event Action<LevelProgress> OnProgressChanged;

    public LevelProgress()
    {
        Level = 1;
        Round = 0;
    }

    public event Action<LevelProgress> OnModelChanged;

    public void IncreaseLevel()
    {
        Level++;
        OnModelChanged.Invoke(this);
    }

    public void IncreaseRound()
    {
        Round++;
        OnModelChanged.Invoke(this);
    }
    
}
