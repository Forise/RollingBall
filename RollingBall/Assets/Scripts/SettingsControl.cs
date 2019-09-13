using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsControl : MonoSingleton<SettingsControl>
{
    #region Fields
    public System.Action settingsChanged;
    [SerializeField]
    public Settings settings = new Settings(true);

    private string path;
    #endregion Fields

    #region Mono Methods
    private void Awake()
    {
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
    path = Application.persistentDataPath + "/Settings.json";
#elif UNITY_EDITOR
        path = Application.dataPath + "/Settings.json";
#endif
        LoadData();
        SaveData();
        Debug.Log(GetSaveDataAsJson());
    }
    #endregion Mono Methods

    #region Methods
    public void SetMusic(bool value)
    {
        settings.isMusicOn = value;
        NotifySettingsChanged();
    }

    private void NotifySettingsChanged()
    {
        settingsChanged?.Invoke();
        SaveData();
    }

    public string GetSaveDataAsJson()
    {
        return JsonUtility.ToJson(settings);
    }

    public void SaveData()
    {
        var json = JsonUtility.ToJson(settings);
        System.IO.File.WriteAllText(path, json);
        //Debug.Log(json);
    }

    private void LoadData()
    {
        if (System.IO.File.Exists(path))
        {
            var json = System.IO.File.ReadAllText(path);
            //Debug.Log("JOSN: " + json);
            settings = JsonUtility.FromJson<Settings>(json);
        }
        else
        {
            SaveData();
            LoadData();
        }
    }
    #endregion Methods
}