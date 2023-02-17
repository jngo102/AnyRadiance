namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when anything in the game tries to set a bool
	/// </summary>
	/// <param name="name">The field being set</param>
	/// <param name="orig">The original value the bool was being set to</param>
	/// <returns>The bool, if overridden, else orig.</returns>
	public delegate bool SetBoolProxy(string name, bool orig);
}