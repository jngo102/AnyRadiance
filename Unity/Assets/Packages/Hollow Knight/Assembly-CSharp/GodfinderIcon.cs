using System.Collections;
using UnityEngine;

public class GodfinderIcon : MonoBehaviour
{
	private static GodfinderIcon instance;

	public AudioSource audioPlayerPrefab;

	public AudioEvent showSound;

	private bool isVisible;

	private MeshRenderer renderer;

	private tk2dSpriteAnimator spriteAnimator;

	private void Awake()
	{
		instance = this;
		renderer = GetComponent<MeshRenderer>();
		spriteAnimator = GetComponent<tk2dSpriteAnimator>();
	}

	private void Start()
	{
		renderer.enabled = false;
	}

	private void Update()
	{
		if (isVisible && !spriteAnimator.Playing)
		{
			Hide();
		}
	}

	public static void ShowIcon(float delay, BossScene bossScene)
	{
		if (GameManager.instance.playerData.GetBool("bossRushMode") || !GameManager.instance.playerData.GetBool("hasGodfinder") || (bossScene != null && bossScene.IsUnlocked(BossSceneCheckSource.Godfinder)))
		{
			return;
		}
		GameManager.instance.playerData.SetBoolSwappedArgs(value: true, "unlockedNewBossStatue");
		if ((bool)instance)
		{
			if (GameManager.instance.GetCurrentMapZone() != "GODS_GLORY")
			{
				instance.StartCoroutine(instance.Show(delay));
			}
			else
			{
				GameManager.instance.playerData.SetBoolSwappedArgs(value: true, "queuedGodfinderIcon");
			}
		}
	}

	public static void ShowIconQueued(float delay)
	{
		if (!GameManager.instance.playerData.GetBool("bossRushMode") && (bool)instance && GameManager.instance.playerData.GetBool("queuedGodfinderIcon"))
		{
			instance.StartCoroutine(instance.Show(delay));
			GameManager.instance.playerData.SetBoolSwappedArgs(value: false, "queuedGodfinderIcon");
		}
	}

	private IEnumerator Show(float delay)
	{
		yield return new WaitForSeconds(delay);
		renderer.enabled = true;
		spriteAnimator.PlayFromFrame(0);
		showSound.SpawnAndPlayOneShot(audioPlayerPrefab, transform.position);
		isVisible = true;
	}

	private void Hide()
	{
		renderer.enabled = false;
		isVisible = false;
	}
}
