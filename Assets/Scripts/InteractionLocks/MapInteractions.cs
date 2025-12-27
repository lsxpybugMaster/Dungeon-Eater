using System;


public class MapInteractions : Singleton<MapInteractions>
{
    public static Action OnMapUIDisabled;
    public static Action OnMapUIEnabled;

    public bool PlayerCanInteract()
    {
        if (GameManager.Instance.GameState == GameState.Exploring)
            return true;
        return false;
    }
}
