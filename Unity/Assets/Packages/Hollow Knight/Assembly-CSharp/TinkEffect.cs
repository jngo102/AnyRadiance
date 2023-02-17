using UnityEngine;

public class TinkEffect : MonoBehaviour
{
	public GameObject blockEffect;

	public bool useNailPosition;

	public bool sendFSMEvent;

	public string FSMEvent;

	public PlayMakerFSM fsm;

	public bool sendDirectionalFSMEvents;

	private BoxCollider2D boxCollider;

	private bool hasBoxCollider;

	private HeroController heroController;

	private GameCameras gameCam;

	private Vector2 centre;

	private float halfWidth;

	private float halfHeight;

	private const float repeatDelay = 0.25f;

	private float nextTinkTime;

	private void Awake()
	{
		boxCollider = base.gameObject.GetComponent<BoxCollider2D>();
	}

	private void Start()
	{
		gameCam = GameCameras.instance;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!(collision.tag == "Nail Attack") || Time.time < nextTinkTime)
		{
			return;
		}
		nextTinkTime = Time.time + 0.25f;
		PlayMakerFSM playMakerFSM = PlayMakerFSM.FindFsmOnGameObject(collision.gameObject, "damages_enemy");
		float degrees = ((playMakerFSM != null) ? playMakerFSM.FsmVariables.FindFsmFloat("direction").Value : 0f);
		if ((bool)gameCam)
		{
			gameCam.cameraShakeFSM.SendEvent("EnemyKillShake");
		}
		Vector3 vector = new Vector3(0f, 0f, 0f);
		Vector3 euler = new Vector3(0f, 0f, 0f);
		Vector3 position = HeroController.instance.transform.position;
		Vector3 position2 = collision.gameObject.transform.position;
		bool flag = boxCollider != null;
		if (useNailPosition)
		{
			flag = false;
		}
		Vector2 vector2 = Vector2.zero;
		float num = 0f;
		float num2 = 0f;
		if (flag)
		{
			vector2 = base.transform.TransformPoint(boxCollider.offset);
			num = boxCollider.bounds.size.x * 0.5f;
			num2 = boxCollider.bounds.size.y * 0.5f;
		}
		switch (DirectionUtils.GetCardinalDirection(degrees))
		{
		case 0:
			HeroController.instance.RecoilLeft();
			if (sendDirectionalFSMEvents)
			{
				fsm.SendEvent("TINK RIGHT");
			}
			vector = ((!flag) ? new Vector3(position.x + 2f, position.y, 0.002f) : new Vector3(vector2.x - num, position2.y, 0.002f));
			break;
		case 1:
			HeroController.instance.RecoilDown();
			if (sendDirectionalFSMEvents)
			{
				fsm.SendEvent("TINK UP");
			}
			vector = ((!flag) ? new Vector3(position.x, position.y + 2f, 0.002f) : new Vector3(position2.x, Mathf.Max(vector2.y - num2, position2.y), 0.002f));
			euler = new Vector3(0f, 0f, 90f);
			break;
		case 2:
			HeroController.instance.RecoilRight();
			if (sendDirectionalFSMEvents)
			{
				fsm.SendEvent("TINK LEFT");
			}
			vector = ((!flag) ? new Vector3(position.x - 2f, position.y, 0.002f) : new Vector3(vector2.x + num, position2.y, 0.002f));
			euler = new Vector3(0f, 0f, 180f);
			break;
		default:
			if (sendDirectionalFSMEvents)
			{
				fsm.SendEvent("TINK DOWN");
			}
			vector = ((!flag) ? new Vector3(position.x, position.y - 2f, 0.002f) : new Vector3(position2.x, Mathf.Min(vector2.y + num2, position2.y), 0.002f));
			euler = new Vector3(0f, 0f, 270f);
			break;
		}
		blockEffect.Spawn(vector, Quaternion.Euler(euler)).GetComponent<AudioSource>().pitch = Random.Range(0.85f, 1.15f);
		if (sendFSMEvent)
		{
			fsm.SendEvent(FSMEvent);
		}
	}
}
