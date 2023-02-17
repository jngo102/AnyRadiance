namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when health is taken from the player
	/// </summary>
	/// <param name="damage">Amount of Damage</param>
	/// <returns>Modified Damaged</returns>
	public delegate int TakeHealthProxy(int damage);
}