// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("2D Toolkit/TextMesh")]
	[Tooltip("Set the colors of a TextMesh. \nChanges will not be updated if commit is OFF. This is so you can change multiple parameters without reconstructing the mesh repeatedly.\n Use tk2dtextMeshCommit or set commit to true on your last change for that mesh. \nNOTE: The Game Object must have a tk2dTextMesh attached.")]
	public class Tk2dTextMeshSetColors : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a tk2dTextMesh component attached.")]
		[CheckForComponent(typeof(tk2dTextMesh))]
		public FsmOwnerDefault gameObject;
		

		[Tooltip("Main color")]
		[UIHint(UIHint.FsmColor)]
		public FsmColor mainColor;
		
		[Tooltip("Gradient color. Only used if gradient is true")]
		[UIHint(UIHint.FsmColor)]
		public FsmColor gradientColor;	
		
		[Tooltip("Use gradient.")]
		[UIHint(UIHint.FsmBool)]		
		public FsmBool useGradient;		
		
		[Tooltip("Commit changes")]
		[UIHint(UIHint.FsmString)]		
		public FsmBool commit;
		
		[ActionSection("")] 
		
		[Tooltip("Repeat every frame.")]
		public bool everyframe;
	
		private tk2dTextMesh _textMesh;
		
		private void _getTextMesh()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			_textMesh =  go.GetComponent<tk2dTextMesh>();
		}
				
		public override void Reset()
		{
			gameObject = null;
			mainColor = null;
			gradientColor = null;
			useGradient = false;
			commit = true;
			everyframe = false;
		}
		
		public override void OnEnter()
		{
			_getTextMesh();
			
			DoSetColors();
			
			if (!everyframe)
			{
				Finish();
			}
		}
		public override void OnUpdate()
		{
			DoSetColors();
		}

		void DoSetColors()
		{

			if (_textMesh == null)
			{
				LogWarning("Missing tk2dTextMesh component: " + _textMesh.gameObject.name);
				return;
			}
			bool dirty = false;
			
			if (_textMesh.useGradient != useGradient.Value)
			{
				_textMesh.useGradient = useGradient.Value;
				dirty = true;
			}
			
			if (_textMesh.color != mainColor.Value)
			{
				_textMesh.color = mainColor.Value;
				dirty = true;
			}
			
			if (_textMesh.color2 != gradientColor.Value)
			{
				_textMesh.color2 = gradientColor.Value;
				dirty = true;
			}	
			
			if (commit.Value && dirty)
			{
				_textMesh.Commit();
			}
		}
		
		/*
		public override string ErrorCheck()
		{
			if (ORSpriteName.Value != "")
			{
			//	id = _sprite.GetSpriteIdByName(ORSpriteName.Value);
			}
			
		}
		*/

	}
}