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
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameManager.Instance.HeroState.GainCoins(5);
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