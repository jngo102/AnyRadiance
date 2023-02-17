using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Hollow Knight")]
public class SetDreamNailConvo : FsmStateAction
{
	[UIHint(UIHint.Variable)]
	public FsmOwnerDefault target;

	public FsmString title;

	public override void Reset()
	{
		target = new FsmOwnerDefault();
		title = new FsmString
		{
			UseVariable = false
		};
	}

	public override void OnEnter()
	{
		GameObject gameObject = ((target.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : target.GameObject.Value);
		if (gameObject != null)
		{
			EnemyDreamnailReaction component = gameObject.GetComponent<EnemyDreamnailReaction>();
			if (component != null && !title.IsNone)
			{
				component.SetConvoTitle(title.Value);
			}
		}
		Finish();
	}
}
