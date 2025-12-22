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
        if (type == GridType.Enemy)
            sr.color = Color.red;
        else if (type == GridType.Rest)
            sr.color = Color.green;
        else
            sr.color = Color.yellow;
    }

    public void SetIndex(int index)
    {
        indexText.text = index.ToString();
    }
}
