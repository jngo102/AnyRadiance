using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Store all resolutions")]
	public class ArrayListGetScreenResolutions : ArrayListActions
	{
		[ActionSection("Set up")]
		[RequiredField]
		[Tooltip("The gameObject with the PlayMaker ArrayList Proxy component")]
		[CheckForComponent(typeof(PlayMakerArrayListProxy))]
		public FsmOwnerDefault gameObject;
	
		[Tooltip("Author defined Reference of the PlayMaker ArrayList Proxy component ( necessary if several component coexists on the same GameObject")]
		public FsmString reference;
	
		public override void Reset()
		{
			gameObject = null;
			reference = null;
		}
	
		public override void OnEnter()
		{
			if (SetUpArrayListProxyPointer(base.Fsm.GetOwnerDefaultTarget(gameObject), reference.Value))
			{
				getResolutions();
			}
			Finish();
		}
	
		public void getResolutions()
		{
			if (isProxyValid())
			{
				proxy.arrayList.Clear();
				Resolution[] resolutions = Screen.resolutions;
				for (int i = 0; i < resolutions.Length; i++)
				{
					Resolution resolution = resolutions[i];
					proxy.arrayList.Add(new Vector3(resolution.width, resolution.height, resolution.refreshRate));
				}
			}
		}
	}
}