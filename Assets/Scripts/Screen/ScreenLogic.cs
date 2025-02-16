using UnityEngine;

public class ScreenLogic : MonoBehaviour, IScreenLogic
{
    [SerializeField] private ScreenReference[] Screens;

    private void Start()
    {
        foreach (var screenRef in Screens)
        {
            screenRef.Screen.SetScreenLogic(this);
        }
    }

    public void ChangeScreen<TScreen>(IScreen from) where TScreen : IScreen
    {
        Debug.Log($"Change Screen to:{typeof(TScreen)}");
        foreach (var screenRef in Screens)
        {
            screenRef.Screen.OnChangeScreen<TScreen>(from);
        }
    }
}