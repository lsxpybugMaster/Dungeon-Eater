using System;
using System.Collections.Generic;

public abstract class PlayerItemController<TData>
{
    protected HeroState heroState;
    public abstract int Size { get; }
    public abstract List<TData> Datas { get; }

    //外部系统与其联系的事件
    public event Action<TData> OnAddToPlayer;

    public PlayerItemController(HeroState heroState)
    { 
        this.heroState = heroState;
    }

    //激活事件的封装,因为event的调用权限仅限于声明它的类,所以需要在基类中提供一个受保护的方法来触发事件,以供子类调用
    protected void Raise_AddToPlayer(TData data)
    {
        OnAddToPlayer?.Invoke(data);
    }

    public abstract void AddToPlayer(TData data);
    public abstract void UpdateItems(TData data);
    public abstract void RemoveFromPlayer(TData data);


    //考虑到PersistUI已经是全局单例了,就不再使用事件通知
    public abstract void PersistUIUpdate();
}
