namespace Modding.Delegates
{
	
	/// <summary>
	///     Called at the end of the take damage function
	/// </summary>
	/// <param name="hazardType"></param>
	/// <param name="damageAmount"></param>
	public delegate int AfterTakeDamageHandler(int hazardType, int damageAmount);
}