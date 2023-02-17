using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	private static object _lock = new object();

	private static bool applicationIsQuitting = false;

	public static T instance
	{
		get
		{
			if (applicationIsQuitting)
			{
				Debug.LogWarning("[Singleton] Instance '" + typeof(T)?.ToString() + "' already destroyed on application quit. Won't create again - returning null.");
				return null;
			}
			lock (_lock)
			{
				if ((Object)_instance == (Object)null)
				{
					_instance = (T)Object.FindObjectOfType(typeof(T));
					if (Object.FindObjectsOfType(typeof(T)).Length > 1)
					{
						Debug.LogError("[Singleton] Something went really wrong  - there should never be more than one singleton! Reopening the scene might fix it.");
						return _instance;
					}
					if ((Object)_instance == (Object)null)
					{
						GameObject gameObject = new GameObject();
						_instance = gameObject.AddComponent<T>();
						gameObject.name = "(singleton) " + typeof(T).ToString();
						Object.DontDestroyOnLoad(gameObject);
						Debug.Log("[Singleton] An instance of " + typeof(T)?.ToString() + " is needed in the scene, so '" + gameObject?.ToString() + "' was created with DontDestroyOnLoad.");
					}
				}
				return _instance;
			}
		}
	}

	public void Awake()
	{
		Debug.Log("TEST1 - AWAKE - " + GetInstanceID());
		if ((Object)_instance == (Object)null)
		{
			Debug.Log("TEST2 - NEW INSTANCE - " + GetInstanceID());
			_instance = base.gameObject as T;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		else if (this != _instance)
		{
			Debug.Log("TEST3 - DESTROYED - " + GetInstanceID());
			Object.DestroyImmediate(base.gameObject);
		}
		else
		{
			Debug.Log("TEST4 - CORRECT INSTANCE - " + GetInstanceID());
		}
	}

	public void OnDestroy()
	{
		applicationIsQuitting = true;
	}
}
