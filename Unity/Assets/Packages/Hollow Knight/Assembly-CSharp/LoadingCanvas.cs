using UnityEngine;

public class LoadingCanvas : MonoBehaviour
{
	[SerializeField]
	[ArrayForEnum(typeof(GameManager.SceneLoadVisualizations))]
	private GameObject[] visualizationContainers;

	private bool isLoading;

	private GameManager.SceneLoadVisualizations loadingVisualization;

	[SerializeField]
	private LoadingSpinner defaultLoadingSpinner;

	[SerializeField]
	private float continueFromSaveDelayAdjustment;

	protected void Start()
	{
		for (int i = 0; i < visualizationContainers.Length; i++)
		{
			GameObject gameObject = visualizationContainers[i];
			if (!(gameObject == null))
			{
				gameObject.SetActive(value: false);
			}
		}
	}

	protected void Update()
	{
		GameManager unsafeInstance = GameManager.UnsafeInstance;
		if (!(unsafeInstance != null) || isLoading == unsafeInstance.IsLoadingSceneTransition)
		{
			return;
		}
		isLoading = unsafeInstance.IsLoadingSceneTransition;
		if (!isLoading)
		{
			return;
		}
		defaultLoadingSpinner.DisplayDelayAdjustment = ((unsafeInstance.LoadVisualization == GameManager.SceneLoadVisualizations.ContinueFromSave) ? continueFromSaveDelayAdjustment : 0f);
		GameObject gameObject = null;
		if (unsafeInstance.LoadVisualization >= GameManager.SceneLoadVisualizations.Default && (int)unsafeInstance.LoadVisualization < visualizationContainers.Length)
		{
			gameObject = visualizationContainers[(int)unsafeInstance.LoadVisualization];
		}
		for (int i = 0; i < visualizationContainers.Length; i++)
		{
			GameObject gameObject2 = visualizationContainers[i];
			if (!(gameObject2 == null))
			{
				gameObject2.SetActive(gameObject2 == gameObject);
			}
		}
	}
}
