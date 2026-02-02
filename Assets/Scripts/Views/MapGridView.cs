using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapGridView : MonoBehaviour
{
    private SpriteRenderer sr;

    public TMP_Text indexText;

    public void Setup(GridType type)
    {
        sr = GetComponent<SpriteRenderer>();

        sr.color = RoomDataBase.GetRoomData(type).RoomColor;
    }

    //动态地图时需要
    public void UpdateView(GridType type)
    {
        sr.color = RoomDataBase.GetRoomData(type).RoomColor;
    }

    public void SetIndex(int index)
    {
        indexText.text = index.ToString();
    }
}
