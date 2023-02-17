using UnityEngine;

namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when anything in the game tries to get a Vector3
	/// </summary>
	/// <param name="name">The name of the Vector3 field</param>
	/// <param name="orig">The original value of the field</param>
	/// <returns>The value to override the vector to</returns>
	public delegate Vector3 GetVector3Proxy(string name, Vector3 orig);
}