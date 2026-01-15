using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : RoomUI
{
    [SerializeField] private ShowDeckUI showDeckUI;

    private void Start()
    {
        showDeckUI.Show();
    }
}
