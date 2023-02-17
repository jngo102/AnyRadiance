using System;
using UnityEngine;

[Serializable]
public struct StartupPool
{
	public int size;

	public GameObject prefab;

	public StartupPool(int size, GameObject prefab)
	{
		this.size = size;
		this.prefab = prefab;
	}
}
