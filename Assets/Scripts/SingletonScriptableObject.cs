using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T[] assets = Resources.LoadAll<T>("");

                if (assets == null || assets.Length < 1)
                {
                    throw new System.Exception("Could not find SingletonScriptableObject of type " + typeof(T).FullName);
                }
                else if (assets.Length > 1)
                {
                    Debug.LogWarning("Found more than one SingletonScriptableObject of type " + typeof(T).FullName);
                }

                _instance = assets[0];
            }

            return _instance;
        }
        private set => _instance = value;
    }
}
