

// ObjectPool
using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectPool : MonoBehaviour
{
	[Serializable]
	public class StartupPool
	{
		public int size;

		public GameObject prefab;
	}

	private static List<GameObject> tempList = new List<GameObject>();

	private static List<GameObject> destroyList = new List<GameObject>();

	private Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>();

	private Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>();

	public StartupPool[] startupPools;

	private bool startupPoolsCreated;

	private static Vector2 activeStashLocation = new Vector2(-20f, -20f);

	private static bool isRecycling;

	private static ObjectPool _instance;

	public static ObjectPool instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType<ObjectPool>();
				if (_instance == null)
				{
					Debug.LogError("Couldn't find an Object Pool, make sure a Game Manager exists in the scene.");
				}
				else
				{
					UnityEngine.Object.DontDestroyOnLoad(_instance.gameObject);
				}
			}
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
		else if (this != _instance)
		{
			Debug.LogErrorFormat("An extra Global Object Pool has been created by {0} please remove this script. Master Object Pool: {1} (Scene: {2} at time: {3})", base.transform.parent.name, _instance.name, Application.loadedLevelName, Time.realtimeSinceStartup);
			if (base.transform.parent.name == "_GameManager")
			{
				Debug.Log("Object Pool instance is no longer set to master object pool, another Object Pool exists in this scene. Instance currently set to : " + _instance.name);
				_instance = this;
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private void Start()
	{
		if (!instance.startupPoolsCreated)
		{
			CreateStartupPools();
			return;
		}
		for (int i = 0; i < startupPools.Length; i++)
		{
			startupPools[i].prefab.CreatePool(startupPools[i].size);
		}
	}

	public static void CreateStartupPools()
	{
		if (instance.startupPoolsCreated)
		{
			return;
		}
		instance.startupPoolsCreated = true;
		StartupPool[] array = instance.startupPools;
		if (array != null && array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				CreatePool(array[i].prefab, array[i].size);
			}
		}
	}

	public static void CreatePool<T>(T prefab, int initialPoolSize) where T : Component
	{
		CreatePool(prefab.gameObject, initialPoolSize);
	}

	public static void CreatePool(GameObject prefab, int initialPoolSize)
	{
		try
		{
			orig_CreatePool(prefab, initialPoolSize);
		}
		catch (NullReferenceException) when (!ModLoader.LoadState.HasFlag(ModLoader.ModLoadState.Preloaded))
		{
		}
	}

	public void RevertToStartState()
	{
		RecycleAll();
		List<GameObject> list = new List<GameObject>();
		foreach (KeyValuePair<GameObject, List<GameObject>> pooledObject in pooledObjects)
		{
			GameObject key = pooledObject.Key;
			List<GameObject> value = pooledObject.Value;
			int num = 0;
			for (int i = 0; i < startupPools.Length; i++)
			{
				StartupPool startupPool = startupPools[i];
				if (startupPool.prefab == key)
				{
					num = startupPool.size;
					break;
				}
			}
			while (value.Count > num)
			{
				UnityEngine.Object.Destroy(value[0]);
				value.RemoveAt(0);
			}
			if (value.Count < num)
			{
				CreatePool(key, num - value.Count);
			}
			else if (num == 0)
			{
				list.Add(key);
			}
		}
		foreach (GameObject item in list)
		{
			pooledObjects.Remove(item);
		}
	}

	public static T Spawn<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component
	{
		return Spawn(prefab.gameObject, parent, position, rotation).GetComponent<T>();
	}

	public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
	{
		return Spawn(prefab.gameObject, null, position, rotation).GetComponent<T>();
	}

	public static T Spawn<T>(T prefab, Transform parent, Vector3 position) where T : Component
	{
		return Spawn(prefab.gameObject, parent, position, Quaternion.identity).GetComponent<T>();
	}

	public static T Spawn<T>(T prefab, Vector3 position) where T : Component
	{
		return Spawn(prefab.gameObject, null, position, Quaternion.identity).GetComponent<T>();
	}

	public static T Spawn<T>(T prefab, Transform parent) where T : Component
	{
		return Spawn(prefab.gameObject, parent, Vector3.zero, Quaternion.identity).GetComponent<T>();
	}

	public static T Spawn<T>(T prefab) where T : Component
	{
		return Spawn(prefab.gameObject, null, Vector3.zero, Quaternion.identity).GetComponent<T>();
	}

	public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
	{
		try
		{
			return orig_Spawn(prefab, parent, position, rotation);
		}
		catch (NullReferenceException) when (!ModLoader.LoadState.HasFlag(ModLoader.ModLoadState.Preloaded))
		{
			return null;
		}
	}

	public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 position)
	{
		return Spawn(prefab, parent, position, Quaternion.identity);
	}

	public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		return Spawn(prefab, null, position, rotation);
	}

	public static GameObject Spawn(GameObject prefab, Transform parent)
	{
		return Spawn(prefab, parent, Vector3.zero, Quaternion.identity);
	}

	public static GameObject Spawn(GameObject prefab, Vector3 position)
	{
		return Spawn(prefab, null, position, Quaternion.identity);
	}

	public static GameObject Spawn(GameObject prefab)
	{
		return Spawn(prefab, null, Vector3.zero, Quaternion.identity);
	}

	public static void Recycle<T>(T obj) where T : Component
	{
		Recycle(obj.gameObject);
	}

	public static void Recycle(GameObject obj)
	{
		if (instance != null && instance.spawnedObjects.TryGetValue(obj, out var value))
		{
			Recycle(obj, value);
			return;
		}
		ObjectPoolAuditor.RecordDespawned(obj, willReuse: false);
		UnityEngine.Object.Destroy(obj);
	}

	private static void Recycle(GameObject obj, GameObject prefab)
	{
		isRecycling = true;
		if (obj != null && prefab != null)
		{
			instance.pooledObjects[prefab].Add(obj);
			instance.spawnedObjects.Remove(obj);
			obj.transform.parent = instance.transform;
			if (obj.GetComponent<ActiveRecycler>() != null)
			{
				obj.transform.SetPosition2D(activeStashLocation);
				FSMUtility.SendEventToGameObject(obj, "A RECYCLE");
			}
			else
			{
				obj.SetActive(value: false);
			}
			ObjectPoolAuditor.RecordDespawned(obj, willReuse: true);
		}
		isRecycling = false;
	}

	public static void RecycleAll<T>(T prefab) where T : Component
	{
		RecycleAll(prefab.gameObject);
	}

	public static void RecycleAll(GameObject prefab)
	{
		foreach (KeyValuePair<GameObject, GameObject> spawnedObject in instance.spawnedObjects)
		{
			if (spawnedObject.Value == prefab)
			{
				tempList.Add(spawnedObject.Key);
			}
		}
		for (int i = 0; i < tempList.Count; i++)
		{
			Recycle(tempList[i]);
		}
		tempList.Clear();
	}

	public static void RecycleAll()
	{
		tempList.AddRange(instance.spawnedObjects.Keys);
		for (int i = 0; i < tempList.Count; i++)
		{
			Recycle(tempList[i]);
		}
		tempList.Clear();
	}

	public static bool IsSpawned(GameObject obj)
	{
		return instance.spawnedObjects.ContainsKey(obj);
	}

	public static int CountPooled<T>(T prefab) where T : Component
	{
		return CountPooled(prefab.gameObject);
	}

	public static int CountPooled(GameObject prefab)
	{
		if (instance.pooledObjects.TryGetValue(prefab, out var value))
		{
			return value.Count;
		}
		return 0;
	}

	public static int CountSpawned<T>(T prefab) where T : Component
	{
		return CountSpawned(prefab.gameObject);
	}

	public static int CountSpawned(GameObject prefab)
	{
		int num = 0;
		foreach (GameObject value in instance.spawnedObjects.Values)
		{
			if (prefab == value)
			{
				num++;
			}
		}
		return num;
	}

	public static int CountAllPooled()
	{
		int num = 0;
		foreach (List<GameObject> value in instance.pooledObjects.Values)
		{
			num += value.Count;
		}
		return num;
	}

	public static List<GameObject> GetPooled(GameObject prefab, List<GameObject> list, bool appendList)
	{
		if (list == null)
		{
			list = new List<GameObject>();
		}
		if (!appendList)
		{
			list.Clear();
		}
		if (instance.pooledObjects.TryGetValue(prefab, out var value))
		{
			list.AddRange(value);
		}
		return list;
	}

	public static List<T> GetPooled<T>(T prefab, List<T> list, bool appendList) where T : Component
	{
		if (list == null)
		{
			list = new List<T>();
		}
		if (!appendList)
		{
			list.Clear();
		}
		if (instance.pooledObjects.TryGetValue(prefab.gameObject, out var value))
		{
			for (int i = 0; i < value.Count; i++)
			{
				list.Add(value[i].GetComponent<T>());
			}
		}
		return list;
	}

	public static List<GameObject> GetSpawned(GameObject prefab, List<GameObject> list, bool appendList)
	{
		if (list == null)
		{
			list = new List<GameObject>();
		}
		if (!appendList)
		{
			list.Clear();
		}
		foreach (KeyValuePair<GameObject, GameObject> spawnedObject in instance.spawnedObjects)
		{
			if (spawnedObject.Value == prefab)
			{
				list.Add(spawnedObject.Key);
			}
		}
		return list;
	}

	public static List<T> GetSpawned<T>(T prefab, List<T> list, bool appendList) where T : Component
	{
		if (list == null)
		{
			list = new List<T>();
		}
		if (!appendList)
		{
			list.Clear();
		}
		GameObject gameObject = prefab.gameObject;
		foreach (KeyValuePair<GameObject, GameObject> spawnedObject in instance.spawnedObjects)
		{
			if (spawnedObject.Value == gameObject)
			{
				list.Add(spawnedObject.Key.GetComponent<T>());
			}
		}
		return list;
	}

	public static void DestroyPooled(GameObject prefab)
	{
		if (instance.pooledObjects.TryGetValue(prefab, out var value))
		{
			for (int i = 0; i < value.Count; i++)
			{
				UnityEngine.Object.Destroy(value[i]);
			}
			value.Clear();
		}
	}

	public static void DestroyPooled<T>(T prefab) where T : Component
	{
		DestroyPooled(prefab.gameObject);
	}

	public static void DestroyPooled(GameObject prefab, int amountToRemove)
	{
		RecycleAll(prefab);
		if (!instance.pooledObjects.TryGetValue(prefab, out var value))
		{
			return;
		}
		for (int i = 0; i < amountToRemove; i++)
		{
			if (value.Count <= 0)
			{
				break;
			}
			UnityEngine.Object.Destroy(value[0]);
			value.RemoveAt(0);
		}
	}

	public static void DestroyPooled<T>(T prefab, int amount) where T : Component
	{
		DestroyPooled(prefab.gameObject, amount);
	}

	public static void DestroyAll(GameObject prefab)
	{
		RecycleAll(prefab);
		DestroyPooled(prefab);
	}

	public static void DestroyAll<T>(T prefab) where T : Component
	{
		DestroyAll(prefab.gameObject);
	}

	public static GameObject orig_Spawn(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
	{
		bool flag = ((prefab.GetComponent<ActiveRecycler>() != null) ? true : false);
		if (instance.pooledObjects.TryGetValue(prefab, out var value))
		{
			GameObject gameObject = null;
			if (value.Count > 0)
			{
				while (gameObject == null && value.Count > 0)
				{
					gameObject = value[0];
					value.RemoveAt(0);
				}
				if (gameObject != null)
				{
					Transform obj = gameObject.transform;
					obj.parent = parent;
					obj.localPosition = position;
					obj.localRotation = rotation;
					if (flag)
					{
						FSMUtility.SendEventToGameObject(gameObject, "A SPAWN");
					}
					else
					{
						gameObject.SetActive(value: true);
					}
					instance.spawnedObjects.Add(gameObject, prefab);
					ObjectPoolAuditor.RecordSpawned(prefab, didInstantiate: false);
					return gameObject;
				}
			}
			Debug.LogWarningFormat("Object Pool attached to {0} has run out of {1} prefabs, Instantiating an additional one.", instance.name, prefab.name);
			gameObject = UnityEngine.Object.Instantiate(prefab);
			Transform obj2 = gameObject.transform;
			obj2.parent = parent;
			obj2.localPosition = position;
			obj2.localRotation = rotation;
			if (flag)
			{
				FSMUtility.SendEventToGameObject(gameObject, "A SPAWN");
			}
			instance.spawnedObjects.Add(gameObject, prefab);
			ObjectPoolAuditor.RecordSpawned(prefab, didInstantiate: true);
			return gameObject;
		}
		if (prefab == null)
		{
			Debug.LogErrorFormat("Object Pool attached to {0} was asked for a NULL prefab.", instance.name);
			return null;
		}
		Debug.LogWarningFormat("Object Pool attached to {0} could not find a copy of {1}, Instantiating a new one.", instance.name, prefab.name);
		CreatePool(prefab.gameObject, 1);
		return Spawn(prefab);
	}

	public static void orig_CreatePool(GameObject prefab, int initialPoolSize)
	{
		ObjectPoolAuditor.RecordPoolCreated(prefab, initialPoolSize);
		if (prefab != null)
		{
			List<GameObject> list = null;
			if (!instance.pooledObjects.ContainsKey(prefab))
			{
				list = new List<GameObject>();
				instance.pooledObjects.Add(prefab, list);
				if (initialPoolSize > 0)
				{
					bool activeSelf = prefab.activeSelf;
					bool flag;
					if (prefab.GetComponent<ActiveRecycler>() != null)
					{
						flag = true;
						prefab.SetActive(value: true);
					}
					else
					{
						flag = false;
						prefab.SetActive(value: false);
					}
					Transform parent = instance.transform;
					while (list.Count < initialPoolSize)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
						gameObject.transform.parent = parent;
						if (flag)
						{
							gameObject.transform.SetPosition2D(activeStashLocation);
						}
						list.Add(gameObject);
					}
					prefab.SetActive(activeSelf);
				}
			}
			else
			{
				list = instance.pooledObjects[prefab];
				if (initialPoolSize > 0)
				{
					int num = list.Count + initialPoolSize;
					bool activeSelf2 = prefab.activeSelf;
					bool flag2;
					if (prefab.GetComponent<ActiveRecycler>() != null)
					{
						flag2 = true;
						prefab.SetActive(value: true);
					}
					else
					{
						flag2 = false;
						prefab.SetActive(value: false);
					}
					Transform parent2 = instance.transform;
					while (list.Count < num)
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate(prefab);
						gameObject2.transform.parent = parent2;
						if (flag2)
						{
							gameObject2.transform.SetPosition2D(activeStashLocation);
						}
						list.Add(gameObject2);
					}
					prefab.SetActive(activeSelf2);
				}
			}
			if (list == null)
			{
				return;
			}
			{
				foreach (GameObject item in list)
				{
					tk2dSprite[] componentsInChildren = item.GetComponentsInChildren<tk2dSprite>(includeInactive: true);
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].ForceBuild();
					}
				}
				return;
			}
		}
		if (prefab == null)
		{
			Debug.LogError("Trying to create an Object Pool for a prefab that is null.");
		}
	}
}
