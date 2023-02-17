using UnityEngine;

public class CorpseChunker : Corpse
{
	[Header("Chunker Variables")]
	public GameObject effects;

	public GameObject chunks;

	protected override void LandEffects()
	{
		if ((bool)body)
		{
			body.velocity = Vector2.zero;
		}
		splatAudioClipTable.SpawnAndPlayOneShot(audioPlayerPrefab, base.transform.position);
		GlobalPrefabDefaults.Instance.SpawnBlood(base.transform.position, 30, 30, 5f, 30f, 60f, 120f);
		GameCameras gameCameras = Object.FindObjectOfType<GameCameras>();
		if ((bool)gameCameras)
		{
			gameCameras.cameraShakeFSM.SendEvent("EnemyKillShake");
		}
		if ((bool)effects)
		{
			effects.SetActive(value: true);
		}
		if ((bool)chunks)
		{
			chunks.SetActive(value: true);
			chunks.transform.SetParent(null, worldPositionStays: true);
			FlingUtils.ChildrenConfig config = default(FlingUtils.ChildrenConfig);
			config.Parent = chunks;
			config.SpeedMin = 15f;
			config.SpeedMax = 20f;
			config.AngleMin = 60f;
			config.AngleMax = 120f;
			config.OriginVariationX = 0f;
			config.OriginVariationY = 0f;
			FlingUtils.FlingChildren(config, base.transform, Vector3.zero);
		}
		meshRenderer.enabled = false;
	}
}
