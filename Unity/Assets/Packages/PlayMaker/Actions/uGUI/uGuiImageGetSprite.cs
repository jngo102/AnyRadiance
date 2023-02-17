using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Gets the source image sprite of a UGui Image component.")]
	public class uGuiImageGetSprite : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		[Tooltip("The GameObject with the Image ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[Tooltip("The source sprite of the UGui Image component.")]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(Sprite))]
		public FsmObject sprite;
	
		private Image _image;
	
		public override void Reset()
		{
			gameObject = null;
			sprite = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_image = ownerDefaultTarget.GetComponent<Image>();
			}
			DoSetImageSourceValue();
			Finish();
		}
	
		private void DoSetImageSourceValue()
		{
			if (_image != null)
			{
				_image.sprite = (Sprite)sprite.Value;
			}
		}
	}
}