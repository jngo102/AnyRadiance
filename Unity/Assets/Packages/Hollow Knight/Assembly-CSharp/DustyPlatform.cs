using UnityEngine;

public class DustyPlatform : MonoBehaviour
{
	private BoxCollider2D bodyCollider;

	[SerializeField]
	private float inset;

	[SerializeField]
	private LayerMask dustIgnoredLayers;

	[SerializeField]
	private RandomAudioClipTable dustFallClips;

	[SerializeField]
	private AudioSource dustFallSourcePrefab;

	[SerializeField]
	private ParticleSystem dustPrefab;

	[SerializeField]
	private ParticleSystem rocksPrefab;

	[SerializeField]
	private float dustRateAreaFactor;

	[SerializeField]
	private float dustRateConstant;

	[SerializeField]
	private GameObject streamPrefab;

	[SerializeField]
	private Vector3 streamOffset;

	[SerializeField]
	private float streamEmissionMin;

	[SerializeField]
	private float streamEmissionMax;

	[SerializeField]
	private float rocksChance;

	[SerializeField]
	private float rocksDelay;

	[SerializeField]
	private Transform rockPrefab;

	[SerializeField]
	private int rockCountMin;

	[SerializeField]
	private int rockCountMax;

	private float rocksDelayTimer;

	[SerializeField]
	private float cooldownDuration;

	private float cooldownTimer;

	private bool isRunning;

	protected void Reset()
	{
		inset = 0.3f;
		dustIgnoredLayers.value = 327680;
		dustRateAreaFactor = 10f;
		dustRateConstant = 5f;
		streamOffset = new Vector3(0f, 0.1f, 0.01f);
		streamEmissionMin = 3f;
		streamEmissionMax = 10f;
		rocksChance = 0.5f;
		rocksDelay = 0.25f;
		rockCountMin = 1;
		rockCountMax = 3;
		cooldownDuration = 0.45f;
	}

	protected void Awake()
	{
		bodyCollider = GetComponent<BoxCollider2D>();
	}

	protected void Update()
	{
		if (!isRunning)
		{
			return;
		}
		bool flag = true;
		if (rocksDelayTimer > 0f)
		{
			rocksDelayTimer -= Time.deltaTime;
			if (rocksDelayTimer <= 0f)
			{
				SpawnRocks();
			}
			else
			{
				flag = false;
			}
		}
		if (cooldownTimer > 0f)
		{
			cooldownTimer -= Time.deltaTime;
			if (cooldownTimer > 0f)
			{
				flag = false;
			}
		}
		if (flag)
		{
			isRunning = false;
		}
	}

	protected void OnCollisionEnter2D(Collision2D collision)
	{
		if (isRunning)
		{
			return;
		}
		int layer = collision.collider.gameObject.layer;
		if ((dustIgnoredLayers.value & (1 << layer)) != 0)
		{
			return;
		}
		Vector2 vector = Vector2.zero;
		if (collision.contacts.Length != 0)
		{
			vector = collision.contacts[0].normal;
		}
		if (!(Mathf.Abs(vector.y - -1f) > 0.1f))
		{
			dustFallClips.SpawnAndPlayOneShot(dustFallSourcePrefab, base.transform.position);
			Vector2 vector2 = bodyCollider.size - new Vector2(inset, inset);
			Vector3 position = base.transform.position;
			position.z = -0.1f;
			if (dustPrefab != null)
			{
				ParticleSystem particleSystem = dustPrefab.Spawn(position);
				SetRateOverTime(particleSystem, vector2.x * vector2.y * dustRateAreaFactor + dustRateConstant);
				particleSystem.transform.localScale = new Vector3(vector2.x, vector2.y, particleSystem.transform.localScale.z);
			}
			if (streamPrefab != null)
			{
				GameObject obj = streamPrefab.Spawn(position + new Vector3(0f, (0f - bodyCollider.size.y) * 0.5f, 0.01f) + streamOffset);
				obj.GetComponentInChildren<ParticleSystem>();
				Vector3 localScale = obj.transform.localScale;
				localScale.x = vector2.x;
				obj.transform.localScale = localScale;
			}
			if (Random.value < rocksChance && rocksPrefab != null)
			{
				ParticleSystem particleSystem2 = rocksPrefab.Spawn(position);
				particleSystem2.transform.position = new Vector3(particleSystem2.transform.position.x, particleSystem2.transform.position.y, 0.003f);
				particleSystem2.transform.localScale = new Vector3(vector2.x, vector2.y, particleSystem2.transform.localScale.z);
			}
			cooldownTimer = cooldownDuration;
			isRunning = true;
		}
	}

	private void SpawnRocks()
	{
	}

	private void SetRateOverTime(ParticleSystem ps, float rateOverTime)
	{
		ParticleSystem.EmissionModule emission = ps.emission;
		emission.rateOverTime = rateOverTime;
	}
}
