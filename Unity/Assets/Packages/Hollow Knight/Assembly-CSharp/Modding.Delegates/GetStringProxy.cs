namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when anything in the game tries to get a string
	/// </summary>
	/// <param name="name">The name of the field</param>
	/// <param name="res">The original value of the field</param>
	/// <returns>The modified value of the get</returns>
	public delegate string GetStringProxy(string name, string res);
}