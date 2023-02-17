using GlobalEnums;
using MonoMod.Utils;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using USceneManager = UnityEngine.SceneManagement.SceneManager;
using Vasi;

namespace AnyRadiance
{
    internal class RadianceFinder : MonoBehaviour
    {
        private static readonly FastReflectionDelegate _updateDelegate =
            typeof(BossStatue)
                .GetMethod("UpdateDetails", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetFastDelegate();

        private void Awake()
        {
            USceneManager.activeSceneChanged += SceneChanged;
        }

        private void SceneChanged(Scene prevScene, Scene nextScene) => StartCoroutine(SceneChangeRoutine(nextScene.name));

        private IEnumerator SceneChangeRoutine(string next)
        {
            if (next == "GG_Workshop") yield return ChangeStatue();
            if (next != "GG_Radiance") yield break;
            if (!PlayerData.instance.statueStateRadiance.usingAltVersion ||
                (!AnyRadiance.Settings.InBossDoor && BossSequenceController.IsInSequence)) 
                yield break;
            
            StartCoroutine(AddComponent());
        }

        private IEnumerator ChangeStatue()
        {
            yield return null;

            GameObject statue = GameObject.Find("GG_Statue_Radiance");

            var scene = ScriptableObject.CreateInstance<BossScene>();
            scene.sceneName = "GG_Radiance";

            var bs = statue.GetComponent<BossStatue>();
            bs.dreamBossScene = scene;
            bs.dreamStatueStatePD = "statueStateAnyRad";
            bs.SetPlaquesVisible(bs.StatueState.isUnlocked && bs.StatueState.hasBeenSeen);

            var details = new BossStatue.BossUIDetails();
            details.nameKey = details.nameSheet = "ANY_RAD_NAME";
            details.descriptionKey = details.descriptionSheet = "ANY_RAD_DESC";
            bs.dreamBossDetails = details;

            statue.Child("dream_version_switch").SetActive(true);

            var toggle = statue.GetComponentInChildren<BossStatueDreamToggle>(true);
            toggle.SetState(true);

            yield return new WaitWhile(() => bs.bossUIControlFSM == null);

            toggle.SetOwner(bs);
            _updateDelegate(bs);

#if DEBUG
            statue.transform.SetPositionX(15);
#endif
        }

        private void OnDestroy()
        {
            USceneManager.activeSceneChanged -= SceneChanged;
        }

        private IEnumerator AddComponent()
        {
            yield return null;

            FindObjectsOfType<GameObject>(true).FirstOrDefault(go => 
                go.name == "Absolute Radiance" && go.layer == (int)PhysLayers.ENEMIES)?
                .AddComponent<Radiance>();
        }
    }
}