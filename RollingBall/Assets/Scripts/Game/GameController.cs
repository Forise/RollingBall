using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    #region Fields
    public System.Action gameStarted;
    public System.Action gameEnded;
    public System.Action gameWon;
    public System.Action gameLost;

    public Level levelPrefab;

    private Level currentLevel;
    private Vector3 mainCameraDefaultPos;
    #endregion Fields

    private void Awake()
    {
        //StartGame();
        mainCameraDefaultPos = Camera.main.transform.position;
    }

    #region Methods
    public void StartGame()
    {
        var cam = Camera.main;
        var camFollower = Camera.main.GetComponent<FollowToObject>();
        currentLevel = Instantiate(levelPrefab);
        currentLevel.player.playerFinished += Win;
        currentLevel.player.playerDied += Lose;
        camFollower.StopFollow();
        cam.transform.position = mainCameraDefaultPos;
        camFollower.SetupObject(currentLevel.player.gameObject);
        gameStarted?.Invoke();
    }

    private void EndGame()
    {
        Destroy(currentLevel.gameObject);
        gameEnded?.Invoke();
    }

    public void Win()
    {
        gameWon?.Invoke();
        UIControl.Instance.ShowDialogPopUp(new UIPopUp.PopupData(
            "You won! Wanna Restart?",
            () =>
            {
                EndGame();
                StartGame();
            },
            () =>
            {
                EndGame();
                UIControl.Instance.OpenWindow("UIMainMenu");
            },
            false,
            true,
            true));
    }

    public void Lose()
    {
        gameLost?.Invoke();
        UIControl.Instance.ShowDialogPopUp(new UIPopUp.PopupData(
            "You lost! Wanna Restart?",
            () =>
            {
                EndGame();
                StartGame();
            },
            () =>
            {
                EndGame();
                UIControl.Instance.OpenWindow("UIMainMenu");
            },
            false,
            true,
            true));
    }
    #endregion Methods
}
