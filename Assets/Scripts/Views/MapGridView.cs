using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridView : MonoBehaviour
{
    private SpriteRenderer sr;

    public void Setup(GridType type)
    {
        sr = GetComponent<SpriteRenderer>();
        if (type == GridType.Enemy)
            sr.color = Color.red;
        else
            sr.color = Color.yellow;
    }
}
