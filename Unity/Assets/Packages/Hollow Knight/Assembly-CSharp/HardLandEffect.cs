using UnityEngine;

public class HardLandEffect : MonoBehaviour
{
	public GameObject dustObj;

	public GameObject grassObj;

	public GameObject boneObj;

	public GameObject spaObj;

	public GameObject metalObj;

	public GameObject wetObj;

	[Space]
	public GameObject particleRockPrefab;

	public GameObject spatterWhitePrefab;

	private float recycleTime;

	private void OnEnable()
	{
		dustObj.SetActive(value: false);
		dustObj.SetActiveChildren(value: true);
		grassObj.SetActive(value: false);
		grassObj.SetActiveChildren(value: true);
		boneObj.SetActive(value: false);
		boneObj.SetActiveChildren(value: true);
		spaObj.SetActive(value: false);
		spaObj.SetActiveChildren(value: true);
		metalObj.SetActive(value: false);
		metalObj.SetActiveChildren(value: true);
		wetObj.SetActive(value: false);
		wetObj.SetActiveChildren(value: true);
		GameCameras.instance.cameraShakeFSM.SendEvent("AverageShake");
		bool flag = false;
		switch (GameManager.instance.playerData.GetInt("environmentType"))
		{
		case 0:
			dustObj.SetActive(value: true);
			flag = true;
			break;
		case 1:
			grassObj.SetActive(value: true);
			flag = true;
			break;
		case 2:
			boneObj.SetActive(value: true);
			flag = true;
			break;
		case 3:
			spaObj.SetActive(value: true);
			if ((bool)spatterWhitePrefab)
			{
				FlingUtils.Config config = default(FlingUtils.Config);
				config.Prefab = spatterWhitePrefab;
				config.AmountMin = 50;
				config.AmountMax = 50;
				config.SpeedMin = 10f;
				config.SpeedMax = 30f;
				config.AngleMin = 85f;
				config.AngleMax = 95f;
				config.OriginVariationX = 1f;
				config.OriginVariationY = 0f;
				FlingUtils.SpawnAndFling(config, base.transform, new Vector3(0f, -0.9f, 0f));
			}
			break;
		case 4:
			metalObj.SetActive(value: true);
			break;
		case 6:
			wetObj.SetActive(value: true);
			break;
		}
		if (flag && (bool)particleRockPrefab)
		{
			FlingUtils.Config config = default(FlingUtils.Config);
			config.Prefab = particleRockPrefab;
			config.AmountMin = 2;
			config.AmountMax = 3;
			config.SpeedMin = 12f;
			config.SpeedMax = 15f;
			config.AngleMin = 95f;
			config.AngleMax = 140f;
			FlingUtils.SpawnAndFling(config, base.transform, new Vector3(0f, -0.9f, 0f));
			config = default(FlingUtils.Config);
			config.Prefab = particleRockPrefab;
			config.AmountMin = 2;
			config.AmountMax = 3;
			config.SpeedMin = 12f;
			config.SpeedMax = 15f;
			config.AngleMin = 40f;
			config.AngleMax = 85f;
			FlingUtils.SpawnAndFling(config, base.transform, new Vector3(0f, -0.9f, 0f));
		}
		recycleTime = Time.time + 1.5f;
	}

	private void Update()
	{
		if (Time.time > recycleTime)
		{
			base.gameObject.Recycle();
		}
	}
}
