using System;
using UnityEngine;

public static class RoomUIEvents
{
    public static Action OnRoomExit;
}

public class RoomUIManager : MonoBehaviour
{
    //先这样统一管理UI, 项目小可以接受
    [SerializeField] private RestUI restUI;
    [SerializeField] private ShopUI shopUI;
    private RoomUI curUI;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
        RoomUIEvents.OnRoomExit += HandleRoomUIExit;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
        RoomUIEvents.OnRoomExit -= HandleRoomUIExit;
    }

    private void HandleGameStateChanged(GameState state)
    {
        Debug.Log("HandleGameStateChanged");
        switch (state)
        {
            case GameState.Resting:
                restUI.Show();
                curUI = restUI;
                break;

            case GameState.Shopping:
                shopUI.Show();
                curUI = shopUI;
                break;

            default:
                curUI?.Hide(); //注意防止curUI空指针                
                break;
        }
    }

    private void HandleRoomUIExit()
    {
        MapDicesSystem.Instance.ResetRollDiceTimes();

        MapInteractions.OnButtonDisabled();

        GameManager.Instance.ChangeGameState(GameState.Exploring);
    }

}
