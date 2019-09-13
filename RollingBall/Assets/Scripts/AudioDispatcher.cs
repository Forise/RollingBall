using UnityEngine;
using System.Collections.Generic;

public class AudioDispatcher : MonoBehaviour
{
    public enum DispatcherType
    {
        Music
    }

    #region Fields
    public List<AudioEvent> startAudioEvents = new List<AudioEvent>();
    public List<AudioEvent> stopAudioEvents = new List<AudioEvent>();

    private Dictionary<string, List<AudioClip>> sortedAudioEvents = new Dictionary<string, List<AudioClip>>();
    private Dictionary<string, TimedNumber> eventTimes = new Dictionary<string, TimedNumber>();
    [SerializeField]
    private DispatcherType dispatcherType;
    #endregion Fields

    #region Mono Methods
    protected virtual void Start()
    {
        foreach (var audioEvent in startAudioEvents)
        {
            if (sortedAudioEvents.ContainsKey(audioEvent.Event))
            {
                sortedAudioEvents[audioEvent.Event].Add(audioEvent.Clip);
            }
            else
            {
                sortedAudioEvents.Add(audioEvent.Event, new List<AudioClip> { audioEvent.Clip });
                eventTimes.Add(audioEvent.Event, new TimedNumber(0f, 0));

                SubscribeStart(audioEvent.Event);
            }
        }

        foreach (var audioEvent in stopAudioEvents)
        {
            SubscribeStop(audioEvent.Event);
        }
    }

    protected virtual void OnDestroy()
    {
        foreach (var startEvent in startAudioEvents)
            UnsubscribeStart(startEvent.Event);
        foreach (var stopEvent in stopAudioEvents)
            UnsubscribeStop(stopEvent.Event);
    }
    #endregion Mono Methods

    #region Methods
    protected virtual void SubscribeStart(string type)
    {
        EventManager.Subscribe(type, Handler_StartAudioEvent);
    }

    protected virtual void UnsubscribeStart(string type)
    {
        EventManager.Unsubscribe(type, Handler_StartAudioEvent);
    }

    protected virtual void SubscribeStop(string type)
    {
        EventManager.Subscribe(type, Handler_StopAudioEvent);
    }

    protected virtual void UnsubscribeStop(string type)
    {
        EventManager.Unsubscribe(type, Handler_StopAudioEvent);
    }
    #endregion Methods

    #region Handlers
    protected virtual void Handler_StartAudioEvent(object sender, GameEventArgs e)
    {
        //Debug.Log($"sound event {e.type} notifyed");
        if (sortedAudioEvents[e.type].Count > 1)
        {
            int oldNumber = eventTimes[e.type].Number;
            do
            {
                eventTimes[e.type].Number = Random.Range(0, sortedAudioEvents[e.type].Count);
            } while (oldNumber == eventTimes[e.type].Number);
        }

        switch (dispatcherType)
        {
            case DispatcherType.Music:
                AudioControl.Instance.PlayMusic(sortedAudioEvents[e.type][eventTimes[e.type].Number], e.boolParam);
                break;
        }

    }

    protected virtual void Handler_StopAudioEvent(object sender, GameEventArgs e)
    {
        foreach (var audioEvent in stopAudioEvents)
        {
            if (e.type == audioEvent.Event)
            {
                switch (dispatcherType)
                {
                    case DispatcherType.Music:
                        AudioControl.Instance.StopMusic();
                        break;
                }
                break;
            }
        }
    }
    #endregion
}

[System.Serializable]
public class AudioEvent
{
    public string Event;
    public UnityEngine.AudioClip Clip;
}

[System.Serializable]
public class TimedNumber
{
    public float Time;
    public int Number;

    public TimedNumber() { }

    public TimedNumber(float time, int number)
    {
        Time = time;
        Number = number;
    }
}