using System;

namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when anything in the game tries to get a generic variable
	/// </summary>
	/// <param name="type">The type of the variable</param>
	/// <param name="name">The field being gotten</param>
	/// <param name="value">The original value of the field</param>
	/// <returns>The modified value</returns>
	public delegate object GetVariableProxy(Type type, string name, object value);
}