using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestUI : RoomUI
{
    [SerializeField] private ShowDeckUI showDeckUI;

    private void Start()
    {
        showDeckUI.Show(GameManager.Instance.HeroState.Deck);
    }
}
