using System;

namespace HutongGames.PlayMaker.Actions
{
	
	[Obsolete("This action is obsolete. Use mask and layers to control subset of transforms in a skeleton")]
	[ActionCategory("Animator")]
	[Tooltip("Returns true if a transform is controlled by the Animator. Can also send events")]
	public class GetAnimatorIsControlled : FsmStateAction
	{
	}
}