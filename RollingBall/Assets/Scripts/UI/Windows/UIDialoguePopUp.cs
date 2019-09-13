using UnityEngine;
using UnityEngine.UI;

public class UIDialoguePopUp : UIPopUp
{
    [SerializeField]
    private Button okButton;
    [SerializeField]
    private Button cancelButton;

    private System.Action okAction;
    private System.Action cancelAction;

    protected override void Start()
    {
        okButton.onClick.AddListener(OkButtonClick);
        cancelButton.onClick.AddListener(CancelButtonClick);
        base.Start();
    }

    private void OnDestroy()
    {
        okButton.onClick.RemoveListener(OkButtonClick);
        cancelButton.onClick.RemoveListener(CancelButtonClick);
    }

    protected override void ActiveStateUpdateHandler()
    {
        base.ActiveStateUpdateHandler();
        CheckMissClick(cancelAction);
    }

    protected override void InactiveStateInitHandler()
    {
        base.InactiveStateInitHandler();
    }

    #region Methods

    protected override void SetContent()
    {
        SetActions();
        base.SetContent();
    }

    private void SetActions()
    {
        okAction = popupData.confirmAction;
        cancelAction = popupData.declineAction;
    }
    #endregion

    #region Handlers
    private void OkButtonClick()
    {
        okAction?.Invoke();
        if (popupData.closeOnOK)
            CloseThisWindow();
    }

    private void CancelButtonClick()
    {
        cancelAction?.Invoke();
        if (popupData.closeOnCancel)
            CloseThisWindow();
    }
    #endregion
}