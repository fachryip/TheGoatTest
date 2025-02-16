public interface IScreenLogic
{
    void ChangeScreen<TScreen>(IScreen from) where TScreen : IScreen;
}
