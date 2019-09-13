using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : UIWindow
{
    #region Fields
    [SerializeField]
    private Toggle musicToggleOn;
    [SerializeField]
    private Toggle musicToggleOff;
    [SerializeField]
    private Button backButton;
    #endregion Fields

    #region Mono Methods
    private void Awake()
    {
        backButton.onClick.AddListener(
            () => 
            {
                UIControl.Instance.OpenWindow("UIMainMenu");
                CloseThisWindow();
            });

        musicToggleOn.onValueChanged.AddListener(
            (value) => 
            {
                SettingsControl.Instance.SetMusic(value);
            });
        musicToggleOff.onValueChanged.AddListener(
            (value) => 
            {
                SettingsControl.Instance.SetMusic(!value);
            });
    }

    protected override void Start()
    {
        base.Start();
        musicToggleOff.isOn = !SettingsControl.Instance.settings.isMusicOn;
        musicToggleOn.isOn = SettingsControl.Instance.settings.isMusicOn;
    }
    #endregion Mono Methods
}
