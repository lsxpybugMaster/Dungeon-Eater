using System;

public abstract class PlayerItemController<TData>
{
    protected HeroState heroState;
    public abstract int Size { get; }

    //外部系统与其联系的事件
    public event Action<TData> OnAddToPlayer;

    public PlayerItemController(HeroState heroState)
    { 
        this.heroState = heroState;
    }

    public abstract void AddToPlayer();
    public abstract void UpdateItems();
    public abstract void RemoveFromPlayer();
}
