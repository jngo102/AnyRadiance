using System;

namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when anything in the game tries to set a generic variable
	/// </summary>
	/// <param name="type">The type of the variable</param>
	/// <param name="name">The name of the field being set</param>
	/// <param name="value">The original value the field was being set to</param>
	/// <returns>The new value of the field</returns>
	public delegate object SetVariableProxy(Type type, string name, object value);
}