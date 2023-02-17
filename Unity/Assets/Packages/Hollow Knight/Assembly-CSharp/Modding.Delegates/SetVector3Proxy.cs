using UnityEngine;

namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when anything in the game tries to set a Vector3
	/// </summary>
	/// <param name="name">The name of the field</param>
	/// <param name="orig">The original value the field was being set to</param>
	/// <returns>The value to override the set to</returns>
	public delegate Vector3 SetVector3Proxy(string name, Vector3 orig);
}