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

        if (Input.GetKeyDown(KeyCode.L))
        {
            LocalizationManager localizationManager = LocalizationManager.Instance;
            Language lang = localizationManager.CurrentLanguage;
            localizationManager.ChangeLanguage(lang == Language.CN ? Language.EN : Language.CN);
        }
    }
}
