using System;
using UnityEngine;

public static class RoomUIEvents
{
    public static Action OnRoomExit;
}

public class RoomUIManager : MonoBehaviour
{

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
        switch (state)
        {
            case GameState.Resting:
                UIManager.Instance.Show<RestUI>();
                break;

            case GameState.Shopping:
                UIManager.Instance.Show<ShopUI>();
                break;

            default:
                UIManager.Instance.Hide<RestUI>();
                UIManager.Instance.Hide<ShopUI>();
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
