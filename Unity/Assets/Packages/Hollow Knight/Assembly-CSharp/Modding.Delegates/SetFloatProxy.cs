namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when anything in the game tries to set a float
	/// </summary>
	/// <param name="name">The field being set</param>
	/// <param name="orig">The original value the float was being set to</param>
	/// <returns>The modified value of the set</returns>
	public delegate float SetFloatProxy(string name, float orig);
}