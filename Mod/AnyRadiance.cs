using Modding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vasi;
using UObject = UnityEngine.Object;
using USceneManager = UnityEngine.SceneManagement.SceneManager;

namespace AnyRadiance
{
    internal class AnyRadiance : Mod, ILocalSettings<LocalSettings>, IMenuMod
    {
        internal static AnyRadiance Instance { get; private set; }

        public Dictionary<string, AudioClip> AudioClips = new();
        public Dictionary<string, AssetBundle> Bundles = new();
        public Dictionary<string, GameObject> GameObjects = new();
        public Dictionary<string, ParticleSystem> Particles = new();

        private Dictionary<string, (string, string)> _preloads = new()
        {
            // ["Boss Scene Controller"] = ("GG_Gruz_Mother", "Boss Scene Controller"),
            // ["Godseeker"] = ("GG_Hornet_1", "GG_Arena_Prefab/Godseeker Crowd"),
            // ["Reference"] = ("GG_Hornet_1", "Boss Holder/Hornet Boss"),
        };

        public static LocalSettings Settings = new();

        public void OnLoadLocal(LocalSettings settings) => Settings = settings;

        public LocalSettings OnSaveLocal() => Settings;

        public AnyRadiance() : base("Any Radiance") { }

        public override List<(string, string)> GetPreloadNames() => _preloads.Values.ToList();

        public override string GetVersion() => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Instance = this;

            foreach (var (name, (scene, path)) in _preloads)
            {
                GameObjects[name] = preloadedObjects[scene][path];
            }

            Unload();

            ModHooks.BeforeSavegameSaveHook += BeforeSaveGameSave;
            ModHooks.AfterSavegameLoadHook += SaveGame;
            ModHooks.SavegameSaveHook += SaveGameSave;
            ModHooks.NewGameHook += AddComponent;
            ModHooks.LanguageGetHook += OnLangGet;
            ModHooks.SetPlayerVariableHook += SetVariableHook;
            ModHooks.GetPlayerVariableHook += GetVariableHook;
        }

        private object SetVariableHook(Type t, string key, object obj)
        {
            if (key != "statueStateAnyRad")
                return obj;

            var completion = (BossStatue.Completion)obj;

            Settings.Completion = completion;

            completion.usingAltVersion = false;

            return completion;
        }

        private object GetVariableHook(Type type, string name, object orig)
        {
            return name == "statueStateAnyRad" ? Settings.Completion : orig;
        }

        private string OnLangGet(string key, string sheettitle, string orig)
        {
            switch (key)
            {
                case "ABSOLUTE_RADIANCE_SUPER":
                    return (PlayerData.instance.statueStateRadiance.usingAltVersion || 
                            (Settings.InBossDoor && BossSequenceController.IsInSequence)) 
                        ? "Any" 
                        : orig;
                case "ANY_RAD_NAME":
                    return "Any Radiance";
                case "ANY_RAD_DESC":
                    return "Why.";
                default:
                    return orig;
            }
        }

        private static void BeforeSaveGameSave(SaveGameData data)
        {
            Settings.UsingAltVersion = PlayerData.instance.statueStateRadiance.usingAltVersion;

            PlayerData.instance.statueStateRadiance.usingAltVersion = false;
        }

        private void SaveGame(SaveGameData data)
        {
            SaveGameSave();
            AddComponent();
            RefreshMenu();
        }

        private static void SaveGameSave(int id = 0)
        {
            PlayerData.instance.statueStateRadiance.usingAltVersion = Settings.UsingAltVersion;
        }

        private static void AddComponent()
        {
            GameManager.instance.gameObject.AddComponent<RadianceFinder>();
        }

        public bool ToggleButtonInsideMenu => true;

        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? menu)
        {
            return new List<IMenuMod.MenuEntry>
            {
                new()
                {
                    Name = "In Pantheon?",
                    Description = "Choose whether Any Radiance shows up in the Pantheon of Hallownest.",
                    Values = new[] { Language.Language.Get("MOH_ON", "MainMenu"), Language.Language.Get("MOH_OFF", "MainMenu") },
                    Saver = ChooseEnter,
                    Loader = () => Settings.InBossDoor ? 0 : 1
                }
            };
        }

        private static void ChooseEnter(int i) => Settings.InBossDoor = i == 0;

        private void RefreshMenu()
        {
            MenuScreen menu = ModHooks.BuiltModMenuScreens[this];

            foreach (var option in menu.gameObject.GetComponentsInChildren<MenuOptionHorizontal>())
            {
                option.menuSetting.RefreshValueFromGameSettings();
            }
        }

        public void Unload()
        {
            ModHooks.BeforeSavegameSaveHook -= BeforeSaveGameSave;
            ModHooks.AfterSavegameLoadHook -= SaveGame;
            ModHooks.SavegameSaveHook -= SaveGameSave;
            ModHooks.NewGameHook -= AddComponent;
            ModHooks.LanguageGetHook -= OnLangGet;
            ModHooks.SetPlayerVariableHook -= SetVariableHook;

            var finder = GameManager.instance.gameObject.GetComponent<RadianceFinder>();

            if (finder != null)
                UObject.Destroy(finder);
        }
    }
}