using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UIPopUp : UIWindow
{
    #region Fields
    [SerializeField]
    protected TMPro.TMP_Text text;
    protected PopupData popupData;
    #endregion
    protected override void Start()
    {
        Init();
        base.Start();
    }
    #region Methods
    public virtual void Init()
    {
        windowObject.SetActive(true);
    }

    public virtual bool Open(PopupData popupData, float time = -1f)
    {        
        if (time > 0)
            StartCoroutine(Countdown(time));
        if (CurrentStateIndex == (int)WindowStates.Inactive)
        {
            this.popupData = popupData;
            closeOnMissClick = popupData.closeOnMissclick;
            SetContent();
            SetState((int)WindowStates.OpenAnimation);
            return true;
        }
        else
        {
            //Debug.LogWarning("Can't open PopUp, because it's already or still opened!");
            return false;
        }
    }

    protected override void Handler_OpenWindow(object sender, GameEventArgs e)
    {
        if (e.str != ID)
            return;
        if (sender.GetType() != typeof(UIControl))
            Debug.LogWarning($"Sender is not UIControl! Sender is {sender.ToString()}", this);
        SetContent();
        SetState((int)WindowStates.OpenAnimation);
    }

    protected override void CloseAnimationStateCloseHandler()
    {
        base.CloseAnimationStateCloseHandler();
        popupData.onCloseAction?.Invoke();
    }

    private IEnumerator Countdown(float time)
    {
        while (time > 0)
        {
            yield return time -= Time.deltaTime;
            if (CurrentStateIndex == (int)WindowStates.CloseAnimation || CurrentStateIndex == (int)WindowStates.Inactive)
            {
                StopCoroutine("Countdown");
                break;
            }
        }
        CloseThisWindow();
        yield return null;
    }

    protected virtual void CheckMissClick(System.Action action)
    {
        if (closeOnMissClick)
        {
            if (Input.GetMouseButton(0))
            {
                if (Input.GetMouseButton(0))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 700f))
                    {
                        if (hit.collider == null || hit.transform.gameObject == null || hit.transform.gameObject.GetComponent<UIPopUp>() == null)
                        {
                            action?.Invoke();
                        }
                    }
                    else
                        action?.Invoke();
                    Debug.DrawRay(ray.origin, ray.direction, Color.red, 700f);
                    Debug.DrawLine(ray.origin, ray.direction, Color.red, 700f);
                }
            }
        }
    }

    protected virtual void SetContent()
    {
        SetTexts();
    }

    protected virtual void SetTexts()
    {
        text.text = popupData.text;

        text.SetText(string.Format(text.text, popupData.intParam));
    }
    #endregion

    public class PopupData
    {
        #region Fields
        public string title;
        public string text;
        public string text2;
        public string imageName;
        public int intParam;
        public bool closeOnMissclick;
        public bool closeOnOK = true;
        public bool closeOnCancel = true;
        public System.Action confirmAction;
        public System.Action confirmAction2;
        public System.Action declineAction;
        public System.Action onCloseAction;
        public Sprite sprite;
        public string[] closeEvents;
        #endregion Fields

        #region Constructors
        public PopupData()
        {
        }

        public PopupData(string text, int intParam = 0, bool closeOnMissclick = true)
        {
            this.text = text;
            this.intParam = intParam;
            this.closeOnMissclick = closeOnMissclick;
        }

        public PopupData(string text, string closeEvent, int intParam = 0, bool closeOnMissclick = true)
        {
            this.text = text;
            this.closeEvents = new string[1] { closeEvent };
            this.intParam = intParam;
            this.closeOnMissclick = closeOnMissclick;
        }

        public PopupData(string text, string closeEvent, System.Action onCloseAction, int intParam = 0, bool closeOnMissclick = true)
        {
            this.text = text;
            this.closeEvents = new string[1] { closeEvent };
            this.intParam = intParam;
            this.onCloseAction = onCloseAction;
            this.closeOnMissclick = closeOnMissclick;
        }

        public PopupData(string text, string[] closeEvents, int intParam = 0, bool closeOnMissclick = true)
        {
            this.text = text;
            this.closeEvents = closeEvents;
            this.intParam = intParam;
            this.closeOnMissclick = closeOnMissclick;
        }

        public PopupData(string text, string[] closeEvents, System.Action onCloseAction, int intParam = 0, bool closeOnMissclick = true)
        {
            this.text = text;
            this.closeEvents = closeEvents;
            this.intParam = intParam;
            this.onCloseAction = onCloseAction;
            this.closeOnMissclick = closeOnMissclick;
        }

        public PopupData(string text, System.Action confirmAction, int intParam = 0, bool closeOnMissclick = true)
        {
            this.text = text;
            this.confirmAction = confirmAction;
            this.intParam = intParam;
            this.closeOnMissclick = closeOnMissclick;
        }

        public PopupData(string text, System.Action confirmAction, System.Action declineAction, bool closeOnMissclick = true, bool closeOnOK = true, bool closeOnCancel = true)
        {
            this.text = text;
            this.confirmAction = confirmAction;
            this.declineAction = declineAction;
            this.closeOnMissclick = closeOnMissclick;
            this.closeOnOK = closeOnOK;
            this.closeOnCancel = closeOnCancel;
        }

        public PopupData(string text, System.Action confirmAction, System.Action declineAction, int intParam = 0, bool closeOnMissclick = true, bool closeOnOK = true)
        {
            this.text = text;
            this.confirmAction = confirmAction;
            this.declineAction = declineAction;
            this.intParam = intParam;
            this.closeOnMissclick = closeOnMissclick;
            this.closeOnOK = closeOnOK;
        }
        #endregion Constructors
    }
}