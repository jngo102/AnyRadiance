using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("uGui")]
	[Tooltip("Sets the texture of a UGui RawImage component.")]
	public class uGuiRawImageSetTexture : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(RawImage))]
		[Tooltip("The GameObject with the RawImage ui component.")]
		public FsmOwnerDefault gameObject;
	
		[RequiredField]
		[Tooltip("The texture of the UGui RawImage component.")]
		public FsmTexture texture;
	
		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;
	
		private RawImage _texture;
	
		private Texture _originalTexture;
	
		public override void Reset()
		{
			gameObject = null;
			texture = null;
			resetOnExit = null;
		}
	
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_texture = ownerDefaultTarget.GetComponent<RawImage>();
			}
			if (resetOnExit.Value)
			{
				_originalTexture = _texture.texture;
			}
			DoSetValue();
			Finish();
		}
	
		private void DoSetValue()
		{
			if (_texture != null)
			{
				_texture.texture = texture.Value;
			}
		}
	
		public override void OnExit()
		{
			if (!(_texture == null) && resetOnExit.Value)
			{
				_texture.texture = _originalTexture;
			}
		}
	}
}