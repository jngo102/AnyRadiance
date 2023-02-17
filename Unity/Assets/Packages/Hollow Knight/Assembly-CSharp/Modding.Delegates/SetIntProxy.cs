namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when anything in the game tries to set an int
	/// </summary>
	/// <param name="name">The field which is being set</param>
	/// <param name="orig">The original value</param>
	/// <returns>The int if overrode, else null</returns>
	public delegate int SetIntProxy(string name, int orig);
}