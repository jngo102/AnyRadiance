namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when damage is dealt to the player
	/// </summary>
	/// <param name="hazardType">The type of hazard that caused the damage.</param>
	/// <param name="damage">Amount of Damage</param>
	/// <returns>Modified Damage</returns>
	public delegate int TakeDamageProxy(ref int hazardType, int damage);
}