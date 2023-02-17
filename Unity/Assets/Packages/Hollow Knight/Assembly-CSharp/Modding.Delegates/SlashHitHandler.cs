using UnityEngine;

namespace Modding.Delegates
{
	
	/// <summary>
	///     Called whenever nail strikes something
	/// </summary>
	/// <param name="otherCollider">What the nail is colliding with</param>
	/// <param name="slash">The NailSlash gameObject</param>
	public delegate void SlashHitHandler(Collider2D otherCollider, GameObject slash);
}