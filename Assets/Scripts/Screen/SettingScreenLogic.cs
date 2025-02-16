using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingScreenLogic : MonoBehaviour, ISettingScreen
{
    [SerializeField] private GameObject SettingCanvasObject;
    [SerializeField] private Text DateText;
    [SerializeField] private Button HomeButton;
    [SerializeField] private Button OpenPageButton;

    private IScreenLogic _screenLogic;

    private void Start()
    {
        SetDateText();
    }

    private void OnEnable()
    {
        HomeButton.onClick.AddListener(ChangeHomeScreen);
        OpenPageButton.onClick.AddListener(OpenNativePage);
    }

    private void OnDisable()
    {
        HomeButton.onClick.RemoveListener(ChangeHomeScreen);
        OpenPageButton.onClick.RemoveListener(OpenNativePage);
    }

    public void OnChangeScreen<TScreen>(IScreen from) where TScreen : IScreen
    {
        var isSetting = typeof(TScreen) == typeof(ISettingScreen);

        SettingCanvasObject.SetActive(isSetting);

        if (isSetting)
        {
            SetDateText();
        }
    }

    public void SetScreenLogic(IScreenLogic screen)
    {
        _screenLogic = screen;
    }

    private void ChangeHomeScreen()
    {
        _screenLogic.ChangeScreen<IHomeScreen>(this);
    }

    private void OpenNativePage()
    {
        UnityInterop.ShowNativePage();
    }

    private void SetDateText()
    {
        var now = DateTime.Now;
        DateText.text = $"Current Date: {now:dddd, dd, MMMM yyyy}. Current Time: {now:HH:mm}";
    }
}