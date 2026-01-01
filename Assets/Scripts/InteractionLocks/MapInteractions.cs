using System;


public class MapInteractions : Singleton<MapInteractions>
{
    public static Action OnButtonEnabled;
    public static Action OnButtonDisabled;

    public bool PlayerCanInteract()
    {
        if (GameManager.Instance.GameState == GameState.Exploring)
            return true;
        return false;
    }
}
