using HutongGames.PlayMaker;

namespace Modding.Delegates
{
	
	/// <summary>
	///     Called when a HitInstance is created in TakeDamage. The hit instance returned defines the hit behavior that will
	///     happen. Overrides default behavior
	/// </summary>
	public delegate HitInstance HitInstanceHandler(Fsm owner, HitInstance hit);
}