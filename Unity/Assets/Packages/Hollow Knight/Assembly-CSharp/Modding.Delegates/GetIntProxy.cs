namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when anything in the game tries to get an int
	/// </summary>
	/// <param name="name">The field being gotten</param>
	/// <param name="orig">The original value of the field</param>
	/// <returns>The int, if overridden, else orig.</returns>
	public delegate int GetIntProxy(string name, int orig);
}