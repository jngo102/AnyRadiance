using HutongGames.PlayMaker;
using UnityEngine;

public class CharmDisplay : MonoBehaviour
{
	public int id;

	public SpriteRenderer spriteRenderer;

	public SpriteRenderer flashSpriteRenderer;

	[Space]
	public Sprite brokenGlassHP;

	public Sprite brokenGlassGeo;

	public Sprite brokenGlassAttack;

	public Sprite whiteCharm;

	public Sprite blackCharm;

	public GameObject charmPlaceEffect;

	private static PlayMakerFSM charmsMenuFsm;

	private bool doJitter;

	private Vector3 startPos;

	private const float jitterX = 0.075f;

	private const float jitterY = 0.075f;

	private void Reset()
	{
		if (!spriteRenderer)
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}
		if (!flashSpriteRenderer)
		{
			Transform transform = base.transform.Find("Flash Sprite");
			if ((bool)transform)
			{
				flashSpriteRenderer = transform.GetComponent<SpriteRenderer>();
			}
		}
	}

	private void Start()
	{
		Sprite sprite = null;
		sprite = ((id == 23 && GameManager.instance.playerData.GetBool("brokenCharm_23")) ? brokenGlassHP : ((id == 24 && GameManager.instance.playerData.GetBool("brokenCharm_24")) ? brokenGlassGeo : ((id == 25 && GameManager.instance.playerData.GetBool("brokenCharm_25")) ? brokenGlassAttack : ((id != 36) ? CharmIconList.Instance.GetSprite(id) : ((GameManager.instance.playerData.GetInt("royalCharmState") > 3) ? blackCharm : whiteCharm)))));
		if ((bool)spriteRenderer)
		{
			spriteRenderer.sprite = sprite;
		}
		if ((bool)flashSpriteRenderer)
		{
			flashSpriteRenderer.sprite = sprite;
		}
		Check();
	}

	public void Check()
	{
		if (charmsMenuFsm == null)
		{
			GameObject gameObject = GameObject.FindWithTag("Charms Pane");
			if ((bool)gameObject)
			{
				charmsMenuFsm = PlayMakerFSM.FindFsmOnGameObject(gameObject, "UI Charms");
			}
		}
		if ((bool)charmsMenuFsm)
		{
			FsmString fsmString = charmsMenuFsm.FsmVariables.FindFsmString("Newly Equipped Name");
			if (fsmString != null && fsmString.Value == base.gameObject.name)
			{
				fsmString.Value = "none";
				if ((bool)charmPlaceEffect)
				{
					charmPlaceEffect.Spawn(base.transform.position + new Vector3(0f, 0f, -1f));
					if ((bool)flashSpriteRenderer)
					{
						flashSpriteRenderer.gameObject.SetActive(value: true);
					}
				}
			}
		}
		if (GameManager.instance.playerData.GetBool("overcharmed"))
		{
			startPos = base.transform.localPosition;
			doJitter = true;
		}
	}

	private void FixedUpdate()
	{
		if (doJitter)
		{
			base.transform.localPosition = new Vector3(startPos.x + Random.Range(-0.075f, 0.075f), startPos.y + Random.Range(-0.075f, 0.075f), startPos.z);
		}
	}
}
