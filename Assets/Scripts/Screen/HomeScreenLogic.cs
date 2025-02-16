using UnityEngine;
using UnityEngine.UI;

public class HomeScreenLogic : MonoBehaviour, IHomeScreen
{
    private const string SparkAction = "spark";

    [SerializeField] private HomeParticleLogic ParticleLogic;
    [SerializeField] private HomeObjectLogic ObjectLogic;
    [SerializeField] private GameObject HomeCanvasObject;
    [SerializeField] private Button SettingButton;

    private IScreenLogic _screenLogic;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        GameLifetimeEvent.OnReady += OnHomeScreenReady;
        GameLifetimeEvent.OnMessageReceive += OnMessageReceived;
        SettingButton.onClick.AddListener(ChangeSettingScreen);
    }

    private void OnDisable()
    {
        GameLifetimeEvent.OnReady -= OnHomeScreenReady;
        GameLifetimeEvent.OnMessageReceive -= OnMessageReceived;
        SettingButton.onClick.RemoveListener(ChangeSettingScreen);
    }

    public void OnChangeScreen<TScreen>(IScreen from) where TScreen : IScreen
    {
        var isHomeScreen = typeof(TScreen) == typeof(IHomeScreen);

        HomeCanvasObject.SetActive(isHomeScreen);
        ObjectLogic.SetActive(isHomeScreen);

        if (isHomeScreen)
        {
            OnHomeScreenReady();
        }
        else
        {
            UnityInterop.StopEmitter();
        }
    }

    public void SetScreenLogic(IScreenLogic screen)
    {
        _screenLogic = screen;
    }

    private void OnHomeScreenReady()
    {
        UnityInterop.SetupEmitter();
        ObjectLogic.SetReady();
    }

    private void OnMessageReceived(string message)
    {
        if (message == SparkAction)
        {
            ParticleLogic.Play();
        }
    }

    private void ChangeSettingScreen()
    {
        _screenLogic.ChangeScreen<ISettingScreen>(this);
    }
}
