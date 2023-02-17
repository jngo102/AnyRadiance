namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when anything in the game tries to get a float
	/// </summary>
	/// <param name="name">The field being set</param>
	/// <param name="orig">The original value</param>
	/// <returns>The value, if overrode, else null.</returns>
	public delegate float GetFloatProxy(string name, float orig);
}