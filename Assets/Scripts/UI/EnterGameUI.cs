using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterGameUI : MonoBehaviour
{
    [SerializeField] private Button EnterGameBtn;

    private void OnEnable()
    {
        EnterGameBtn.onClick.AddListener(EnterGame);

        //GameManager.OnReturnToMenu +=  
    }

    private void OnDisable()
    {
        EnterGameBtn.onClick.RemoveListener(EnterGame);
    }

    public void EnterGame()
    {
        GameManager.Instance.ToBattle();
    }
}
