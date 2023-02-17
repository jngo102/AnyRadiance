using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Hollow Knight")]
	[ActionTarget(typeof(GameObject), "gameObject", true)]
	[Tooltip("Creates a Game Object, usually using a Prefab.")]
	public class CreateUIMsgGetItem : FsmStateAction
	{
		[RequiredField]
		[Tooltip("GameObject to create. Usually a Prefab.")]
		public FsmGameObject gameObject;
	
		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally store the created object.")]
		public FsmGameObject storeObject;
	
		[ObjectType(typeof(Sprite))]
		public FsmObject sprite;
	
		public override void Reset()
		{
			gameObject = null;
			storeObject = null;
			sprite = new FsmObject
			{
				UseVariable = true
			};
		}
	
		public override void OnEnter()
		{
			GameObject value = this.gameObject.Value;
			if (value != null)
			{
				GameObject gameObject = Object.Instantiate(value, Vector3.zero, Quaternion.identity);
				storeObject.Value = gameObject;
				Sprite sprite = this.sprite.Value as Sprite;
				if ((bool)sprite)
				{
					Transform transform = gameObject.transform.Find("Icon");
					if ((bool)transform)
					{
						SpriteRenderer component = transform.GetComponent<SpriteRenderer>();
						if ((bool)component)
						{
							component.sprite = sprite;
						}
					}
				}
			}
			Finish();
		}
	}
}