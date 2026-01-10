using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSystem : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            AddCard();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            UIManager.Instance.Show<RestUI>();
        }
    }

    private void AddCard()
    {
        CardData d = CardDatabase.GetRandomCard();
        DebugUtil.Cyan($"Add Card: {d.name}");
        GameManager.Instance.PlayerDeckController.AddCardToDeck(d);
    }

    private void CreateCardView()
    {
        CardData d = CardDatabase.GetRandomCard();
        Card cd = new Card(d);

        CardViewCreator.Instance.CreateCardView(cd, Vector3.zero, Quaternion.identity);
    }
}
//TODO:
//BUG:
//FIXME:
//OPTIMIZE:
//NOTE:
//DISCUSS:
//STEP:
//IMPORTANT:
//IDEA:
//DELETE: