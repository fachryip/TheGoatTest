public interface IScreen
{
    void SetScreenLogic(IScreenLogic screen);

    void OnChangeScreen<TScreen>(IScreen from) where TScreen : IScreen;
}
