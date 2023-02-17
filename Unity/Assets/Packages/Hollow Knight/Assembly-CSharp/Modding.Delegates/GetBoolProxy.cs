namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when anything in the game tries to get a bool
	/// </summary>
	/// <param name="name">The field being gotten</param>
	/// <param name="orig">The original value of the bool</param>
	/// <returns>The bool, if you are overriding it, otherwise orig.</returns>
	public delegate bool GetBoolProxy(string name, bool orig);
}