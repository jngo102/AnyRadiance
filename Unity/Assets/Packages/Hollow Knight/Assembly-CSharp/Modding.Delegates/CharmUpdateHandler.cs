namespace Modding.Delegates
{
	
	/// <summary>
	///     Called after player values for charms have been set
	/// </summary>
	/// <param name="data">Current PlayerData</param>
	/// <param name="controller">Current HeroController</param>
	public delegate void CharmUpdateHandler(PlayerData data, HeroController controller);
}