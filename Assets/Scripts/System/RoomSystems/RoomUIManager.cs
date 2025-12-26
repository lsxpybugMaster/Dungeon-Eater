using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUIManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
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
        //HideAll();

        //switch (state)
        //{
        //    case GameState.Resting:
        //        restUI.Show();
        //        break;
        //    case GameState.Shopping:
        //        shopUI.Show();
        //        break;
        //}
    }

    //private void HideAll()
    //{
    //    restUI.Hide();
    //    shopUI.Hide();
    //}
}
