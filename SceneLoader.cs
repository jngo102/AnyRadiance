using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using USceneManager = UnityEngine.SceneManagement.SceneManager;

namespace AnyRadiance
{
    internal class SceneLoader : MonoBehaviour
    {
        private void Awake()
        {
            On.GameManager.EnterHero += OnEnterHero;
            USceneManager.activeSceneChanged += OnSceneChange;
        }

        private void OnEnterHero(On.GameManager.orig_EnterHero orig, GameManager gm, bool additiveGateSearch)
        {
            if (gm.sceneName == "GG_TrueRadiance")
            {
                //GameObject.Find("True Radiance").AddComponent<TrueRadiance.TrueRadiance>();
                //GameObject.Find("Portals").AddComponent<PortalManager>();
            }

            orig(gm, additiveGateSearch);

            if (gm.sceneName == "GG_TrueRadiance")
            {
                HeroController.instance.transform.SetPosition2D(17, 6);
                //GameCameras.instance.tk2dCam.ZoomFactor = 0.5f;
                HeroController.instance.transform.Find("Vignette").gameObject.SetActive(false);
            }
        }

        private void OnSceneChange(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "GG_TrueRadiance")
            {
                var bsc = Instantiate(AnyRadiance.Instance.GameObjects["Boss Scene Controller"]);
                bsc.SetActive(true);

                var rootGOs = nextScene.GetRootGameObjects();
                foreach (var go in rootGOs)
                {
                    go.GetComponentsInChildren<SpriteRenderer>(true).ToList()
                        .ForEach(rend => rend.material.shader = Shader.Find("Sprites/Default"));
                    go.GetComponentsInChildren<MeshRenderer>(true).ToList().ForEach(rend =>
                        rend.material.shader = Shader.Find(rend.GetComponent<BlurPlane>()
                            ? "UI/Blur/UIBlur"
                            : "Sprites/Default-ColorFlash"));
                    go.GetComponentsInChildren<TilemapRenderer>(true).ToList()
                        .ForEach(rend => rend.material.shader = Shader.Find("Sprites/Default"));
                }
            }
        }

        private void OnDestroy()
        {
            On.GameManager.EnterHero -= OnEnterHero;
        }
    }
}