namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("ArrayMaker/ArrayList")]
	[Tooltip("Reverses the sequence of elements in a PlayMaker ArrayList Proxy component")]
	public class ArrayListReverse : ArrayListActions
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
				DoArrayListReverse();
			}
			Finish();
		}
	
		public void DoArrayListReverse()
		{
			if (isProxyValid())
			{
				proxy.arrayList.Reverse();
			}
		}
	}
}