using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modding.Utils
{
	
	/// <summary>
	/// Class containing extensions used by the Modding API for interacting with Unity types.
	/// </summary>
	public static class UnityExtensions
	{
		/// <summary>
		/// Get the component of type T attached to GameObject go. If go does not have such a component, add
		/// that component (and return it).
		/// </summary>
		public static T GetOrAddComponent<T>(this GameObject go) where T : Component
		{
			T val = go.GetComponent<T>();
			if ((UnityEngine.Object)val == (UnityEngine.Object)null)
			{
				val = go.AddComponent<T>();
			}
			return val;
		}
	
		/// <summary>
		/// Find a game object by name in the scene. The object's name must be given in the hierarchy.
		/// </summary>
		/// <param name="scene">The scene to search.</param>
		/// <param name="objName">The name of the object in the hierarchy, with '/' separating parent GameObjects from child GameObjects.</param>
		/// <returns>The GameObject if found; null if not.</returns>
		/// <exception cref="T:System.ArgumentException">Thrown if the path to the game object is invalid.</exception>
		public static GameObject FindGameObject(this Scene scene, string objName)
		{
			return GetGameObjectFromArray(scene.GetRootGameObjects(), objName);
		}
	
		internal static GameObject GetGameObjectFromArray(GameObject[] objects, string objName)
		{
			string text = null;
			int num = objName.IndexOf('/');
			string rootName;
			if (num == -1)
			{
				rootName = objName;
			}
			else
			{
				if (num == 0 || num == objName.Length - 1)
				{
					throw new ArgumentException("Invalid GameObject path");
				}
				rootName = objName.Substring(0, num);
				text = objName.Substring(num + 1);
			}
			GameObject gameObject = Enumerable.FirstOrDefault(objects, (GameObject o) => o.name == rootName);
			if (gameObject == null)
			{
				return null;
			}
			if (text != null)
			{
				Transform transform = gameObject.transform.Find(text);
				if (!(transform == null))
				{
					return transform.gameObject;
				}
				return null;
			}
			return gameObject;
		}
	}
}