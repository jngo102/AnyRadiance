using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	
	[ActionCategory("Combat")]
	[Tooltip("Detect 2D entry collisions or triggers between the Owner of this FSM and other Game Objects that have a Damager FSM.")]
	public class ReceivedDamage : FsmStateAction
	{
		[UIHint(UIHint.Tag)]
		[Tooltip("Filter by Tag.")]
		public FsmString collideTag;
	
		[RequiredField]
		[Tooltip("Event to send if a collision is detected.")]
		public FsmEvent sendEvent;
	
		[Tooltip("Name of FSM to look for on colliding object.")]
		public FsmString fsmName;
	
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
		public FsmGameObject storeGameObject;
	
		[Tooltip("Ignore damage from Acid")]
		public FsmBool ignoreAcid;
	
		[Tooltip("Ignore damage from Water")]
		public FsmBool ignoreWater;
	
		private PlayMakerUnity2DProxy _proxy;
	
		public override void Reset()
		{
			collideTag = new FsmString
			{
				UseVariable = true
			};
			sendEvent = null;
			storeGameObject = null;
		}
	
		public override void OnEnter()
		{
			_proxy = base.Owner.GetComponent<PlayMakerUnity2DProxy>();
			if (_proxy == null)
			{
				_proxy = base.Owner.AddComponent<PlayMakerUnity2DProxy>();
			}
			_proxy.AddOnCollisionEnter2dDelegate(DoCollisionEnter2D);
			_proxy.AddOnTriggerEnter2dDelegate(DoTriggerEnter2D);
			_proxy.AddOnTriggerStay2dDelegate(DoTriggerStay2D);
		}
	
		public override void OnExit()
		{
			if (!(_proxy == null))
			{
				_proxy.RemoveOnCollisionEnter2dDelegate(DoCollisionEnter2D);
				_proxy.RemoveOnTriggerEnter2dDelegate(DoTriggerEnter2D);
				_proxy.RemoveOnTriggerStay2dDelegate(DoTriggerStay2D);
			}
		}
	
		public new void DoCollisionEnter2D(Collision2D collisionInfo)
		{
			if ((collisionInfo.collider.gameObject.tag == collideTag.Value || collideTag.IsNone || string.IsNullOrEmpty(collideTag.Value)) && (!ignoreAcid.Value || collisionInfo.gameObject.tag != "Acid") && (!ignoreWater.Value || collisionInfo.gameObject.tag != "Water Surface"))
			{
				StoreCollisionInfo(collisionInfo);
			}
		}
	
		public new void DoTriggerEnter2D(Collider2D collisionInfo)
		{
			if ((collisionInfo.gameObject.tag == collideTag.Value || collideTag.IsNone || string.IsNullOrEmpty(collideTag.Value)) && (!ignoreAcid.Value || collisionInfo.gameObject.tag != "Acid") && (!ignoreWater.Value || collisionInfo.gameObject.tag != "Water Surface"))
			{
				StoreTriggerInfo(collisionInfo);
			}
		}
	
		public new void DoTriggerStay2D(Collider2D collisionInfo)
		{
			if ((collisionInfo.gameObject.tag == collideTag.Value || collideTag.IsNone || string.IsNullOrEmpty(collideTag.Value)) && (!ignoreAcid.Value || collisionInfo.gameObject.tag != "Acid") && (!ignoreWater.Value || collisionInfo.gameObject.tag != "Water Surface"))
			{
				StoreTriggerInfo(collisionInfo);
			}
		}
	
		private void StoreCollisionInfo(Collision2D collisionInfo)
		{
			storeGameObject.Value = collisionInfo.gameObject;
			StoreIfDamagingObject(collisionInfo.gameObject);
		}
	
		private void StoreTriggerInfo(Collider2D collisionInfo)
		{
			storeGameObject.Value = collisionInfo.gameObject;
			StoreIfDamagingObject(collisionInfo.gameObject);
		}
	
		private void StoreIfDamagingObject(GameObject go)
		{
			PlayMakerFSM playMakerFSM = FSMUtility.LocateFSM(go, fsmName.Value);
			if (playMakerFSM != null && playMakerFSM.FsmVariables.GetFsmInt("damageDealt").Value > 0)
			{
				storeGameObject.Value = go;
				base.Fsm.Event(sendEvent);
			}
		}
	
		public override string ErrorCheck()
		{
			string text = string.Empty;
			if (base.Owner != null && base.Owner.GetComponent<Collider2D>() == null && base.Owner.GetComponent<Rigidbody2D>() == null)
			{
				text += "Owner requires a RigidBody2D or Collider2D!\n";
			}
			return text;
		}
	}
}