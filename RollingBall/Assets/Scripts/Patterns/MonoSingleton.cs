using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Fields
    private static T instance;

    private static bool wasDestroyed;
    #endregion Fields

    #region Properties
    public static T Instance
    {
        get
        {
            if (wasDestroyed)
                return null;

            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    GameObject gameObject = new GameObject(typeof(T).Name);
                    DontDestroyOnLoad(gameObject);

                    instance = gameObject.AddComponent(typeof(T)) as T;
                }
            }
            return instance;
        }
    }
    #endregion Properties

    #region Mono Methods
    protected virtual void OnDestroy()
    {
        if (instance)
            Destroy(instance);

        instance = null;
        wasDestroyed = true;
    }
    #endregion Mono Methods
}
