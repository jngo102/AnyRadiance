using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;

public class SendEnemyMessageTrigger : MonoBehaviour
{
	[UnityEngine.Tooltip("If there is an enemy_message FSM on this gameobject, this value will be gotten from it.")]
	public string eventName = "";

	private List<GameObject> enteredEnemies = new List<GameObject>();

	private void Start()
	{
		PlayMakerFSM playMakerFSM = PlayMakerFSM.FindFsmOnGameObject(base.gameObject, "enemy_message");
		if (playMakerFSM != null)
		{
			FsmString fsmString = playMakerFSM.FsmVariables.FindFsmString("Event");
			if (fsmString != null)
			{
				eventName = fsmString.Value;
			}
			playMakerFSM.enabled = false;
		}
	}

	private void FixedUpdate()
	{
		enteredEnemies.Clear();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		GameObject item = collision.attachedRigidbody.gameObject;
		if (!enteredEnemies.Contains(item))
		{
			enteredEnemies.Add(item);
			SendEvent(collision.gameObject);
		}
	}

	private void SendEvent(GameObject obj)
	{
		if (!(eventName != ""))
		{
			return;
		}
		FSMUtility.SendEventToGameObject(obj, eventName);
		if (!string.IsNullOrEmpty(eventName))
		{
			switch (eventName)
			{
			case "GO LEFT":
				SendWalkerGoInDirection(obj, -1);
				break;
			case "GO RIGHT":
				SendWalkerGoInDirection(obj, 1);
				break;
			}
		}
	}

	private static void SendWalkerGoInDirection(GameObject target, int facing)
	{
		Walker component = target.GetComponent<Walker>();
		if (component != null)
		{
			component.RecieveGoMessage(facing);
		}
	}
}
