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
            CardData d = CardDatabase.GetRandomCard();
            Card cd = new Card(d);

            CardViewCreator.Instance.CreateCardView(cd,Vector3.zero,Quaternion.identity);
        }
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