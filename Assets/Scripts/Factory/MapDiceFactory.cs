using UnityEngine;
using System;

//由于MapDice的实例化较为复杂,因此使用工厂类封装
public class MapDiceFactory
{
    private readonly MapDiceView prefab;
    private readonly Transform parent;

    //利用DI将预制体和父节点传入,之后上层模块就等着实例化对象
    public MapDiceFactory(MapDiceView prefab, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
    }

    public MapDiceView Create(
        MapDice mapDice, //数据
        Action<MapDiceView> onClicked, //点击时绑定的事件(基于事件的IoC)
        Action<MapDiceView> onMoveFinished
    ){
        MapDiceView view = UnityEngine.Object.Instantiate(prefab, parent);
        view.MapDice = mapDice;
        view.transform.position = mapDice.start_pos;

        view.OnDiceClicked += onClicked;
        view.OnDiceMoveFinished += onMoveFinished;

        //DISCUSS: 这里再次调用Init是因为要保证工厂类职责单一,仅负责注入
        view.Init();
        return view;
    }
}
