using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIWindow
{
    #region Fields
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button settingsButton;
    #endregion Fields

    #region Mono Methods
    private void Awake()
    {
        playButton.onClick.AddListener(
            () => 
            {
                GameController.Instance.StartGame();
                CloseThisWindow();
            });
        settingsButton.onClick.AddListener(
            () => 
            {
                UIControl.Instance.OpenWindow("UISettings");
                CloseThisWindow();
            });
    }
    #endregion Mono Methods
}
