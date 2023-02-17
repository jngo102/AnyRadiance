namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when anything in the game tries to set a string
	/// </summary>
	/// <param name="name">The name of the field</param>
	/// <param name="res">The original set value of the string</param>
	/// <returns>The modified value of the set</returns>
	public delegate string SetStringProxy(string name, string res);
}