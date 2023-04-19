using AnyRadiance.Actions;
using AnyRadiance.Radiance.Actions;
using HKAIFramework;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;
using Modding.Utils;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Vasi;
using Action = HKAIFramework.Action;
using Random = UnityEngine.Random;

namespace AnyRadiance.Radiance
{
    internal class Radiance : MonoBehaviour
    {
        #region Constants

        private const int NumPlatsA2 = 7;
        private const int NumPlatsAscend = 14;
        private const float FinalPhaseY = 152;
#if DEBUG
        private const int Phase1HP = 100;
        private const int Phase2HP = 50;
        private const int MegaOrbHP = 51;
        private const int Phase3HP = 150;
        private const int Phase3TrailNail2HP = 100;
        private const int Phase3TrailNail3HP = 50;
#else
        private const int Phase1HP = 2000;
        private const int Phase2HP = 500;
        private const int MegaOrbHP = 150;
        private const int Phase3HP = 1500;
        private const int Phase3TrailNail2HP = 800;
        private const int Phase3TrailNail3HP = 350;
#endif
        #endregion

        #region Components

        private tk2dSpriteAnimator _animator;
        private PolygonCollider2D _collider;
        private HealthManager _healthManager;
        private MeshRenderer _renderer;
        private Rigidbody2D _rigidbody;
        #endregion

        #region Other Game Objects and Components

        private GameObject _bossCtrl;
        private PlayMakerFSM _haloFSM;
        private HeroController _hc;
        private PlayMakerFSM _spellCtrl;
        #endregion

        private byte _phase;
        private byte Phase
        {
            get => _phase;
            set
            {
                _phase = value;
                ArenaInfo.SetPhase(_phase);
            }
        }
        private static Coroutine _logic;
        public static GameObject Self { get; private set; }

        private static readonly ActionSequence _nailWalls = new()
        {
            Name = "Nail Walls",
            Actions = new Action[]
            {
                new NailWalls(),
            }
        };

        private static readonly ActionSequence _laserColumns = new()
        {
            Name = "Laser Columns",
            Actions = new Action[]
            {
                new LaserColumns(),
            }
        };

        private static readonly ActionSequence _summonSplitterP1 = new()
        {
            Name = "Summon Splitter",
            Actions = new Action[]
            {
                new SummonSplitter(2.0f / 3, 0.5f, 1.5f, 12, 2, 2, 1.5f, 2),
            }
        };

        private static readonly ActionSequence _summonBeamOrbP1 = new()
        {
            Name = "Summon Beam Orb",
            Actions = new Action[]
            {
                new SummonBeamOrb(true, false, 30, 0.25f, 4, 4, 0.5f, 2),
            }
        };

        private static readonly ActionSequence _nailBarrage = new()
        {
            Name = "Nail Barrage",
            Actions = new Action[]
            {
                new NailBarrage(),
            }
        };

        private static readonly ActionSequence _summonBeamOrbP2 = new()
        {
            Name = "Summon Beam Orb",
            Actions = new Action[]
            {
                new SummonBeamOrb(true, true, 45, 0.1f, 6, 5, 1, float.MaxValue, true),
            }
        };

        private static readonly ActionSequence _summonSplitterP2 = new()
        {
            Name = "Summon Splitter",
            Actions = new Action[]
            {
                new SummonSplitter(1, 0.5f, 2, 12, 2, 2, 2, 2),
            }
        };

        private static readonly ActionSequence _summonMegaOrb = new()
        {
            Name = "Summon Mega Orb",
            Actions = new Action[]
            {
                new SummonMegaOrb(0.25f, 5),
            }
        };

        private readonly ActionSequence _summonTrailNail = new()
        {
            Name = "Summon Trail Nail",
            Actions = new Action[]
            {
                new SummonTrailNail(0.5f, 1, 12, 0.75f),
            }
        };

        private readonly ActionSet _phase1Subphase1Actions = new()
        {
            TrackedSequences = new Tuple<ActionSequence, float, int>[]
            {
                new(_nailWalls, 0.5f, 2),
                new(_laserColumns, 0.5f, 2),
            }
        };

        private readonly ActionSet _phase1Subphase2Actions = new()
        {
            TrackedSequences = new Tuple<ActionSequence, float, int>[]
            {
                new(_summonSplitterP1, 1.0f / 3, 2),
                new(_summonBeamOrbP1, 1.0f / 3, 2),
            }
        };

        private readonly ActionSet _phase2Actions = new()
        {
            TrackedSequences = new Tuple<ActionSequence, float, int>[]
            {
                new(_summonSplitterP2, 0.5f, 2),
                new(_nailBarrage, 0.5f, 2),
            }
        };

        private void Awake()
        {
            Self = gameObject;

            _animator = GetComponent<tk2dSpriteAnimator>();
            _collider = GetComponent<PolygonCollider2D>();
            _healthManager = GetComponent<HealthManager>();
            _renderer = GetComponent<MeshRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();

            _healthManager.hp = Phase1HP;

            GetAudioClips();

            _hc = HeroController.instance;

            _spellCtrl = _hc.gameObject.LocateMyFSM("Spell Control");
            var quake1Down = _spellCtrl.GetAction<CallMethodProper>("Quake1 Down");
            var quake2Down = _spellCtrl.GetAction<CallMethodProper>("Quake2 Down");
            ReflectionHelper.SetField<FsmStateAction, bool>(quake1Down, "enabled", false);
            ReflectionHelper.SetField<FsmStateAction, bool>(quake2Down, "enabled", false);

            foreach (var fsm in GetComponents<PlayMakerFSM>()) Destroy(fsm);

            GetChildren();

            HeroController.instance.gameObject.GetOrAddComponent<Knight>();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.4f);
            _logic = StartCoroutine(StartPhase1());
        }

        private void OnDestroy()
        {
            var quake1Down = _spellCtrl.GetAction<CallMethodProper>("Quake1 Down");
            var quake2Down = _spellCtrl.GetAction<CallMethodProper>("Quake2 Down");
            ReflectionHelper.SetField<FsmStateAction, bool>(quake1Down, "enabled", true);
            ReflectionHelper.SetField<FsmStateAction, bool>(quake2Down, "enabled", true);

            var knight = HeroController.instance.GetComponent<Knight>();
            if (knight != null) Destroy(knight);

            foreach (var attack in FindObjectsOfType<GameObject>().Where(go =>
                     {
                         return go.name.Contains("Radiant Orb") ||
                                go.name.Contains("Radiant Spike") ||
                                go.name.Contains("Radiant Nail") ||
                                go.name.Contains("Radiant Beam") ||
                                go.name.Contains("Beam Orb");
                     }))
            {
                Destroy(attack);
            }

            On.HealthManager.Die -= StartPhase1Death;
            On.HealthManager.Die -= StartPhase2Death;
            On.HealthManager.Die -= StartPhase3Death;
        }

        private void StartPhase1Death(On.HealthManager.orig_Die orig, HealthManager self, float? attackDirection, AttackTypes attackType, bool ignoreEvasion)
        {
            orig(self, attackDirection, attackType, ignoreEvasion);
            if (self.name != "Absolute Radiance") return;
            StopAllCoroutines();
            StartCoroutine(Phase1Death());
        }

        private void StartPhase2Death(On.HealthManager.orig_Die orig, HealthManager self, float? attackDirection, AttackTypes attackType, bool ignoreEvasion)
        {
            orig(self, attackDirection, attackType, ignoreEvasion);
            if (self.name != "Absolute Radiance") return;
            StopAllCoroutines();
            StartCoroutine(Phase2Death());
        }

        private void StartPhase3Death(On.HealthManager.orig_Die orig, HealthManager self, float? attackDirection, AttackTypes attackType, bool ignoreEvasion)
        {
            orig(self, attackDirection, attackType, ignoreEvasion);
            if (self.name != "Absolute Radiance") return;
            StopAllCoroutines();
            StartCoroutine(Phase3Death());
        }

        private IEnumerator Phase1Death()
        {
            StopCoroutine(_logic);
            var shakeFSM = GameCameras.instance.cameraShakeFSM.Fsm;
            GameManager.instance.gameObject.Child("GlobalPool/Stun Effect(Clone)").Spawn(transform.position);
            AnyRadiance.Instance.AudioClips["Knock Down"].PlayOneShot(transform.position);
            shakeFSM.GetFsmBool("RumblingMed").Value = false;
            CamShake("BigShake");
            _haloFSM.SendEvent("DOWN");
            foreach (var attack in FindObjectsOfType<GameObject>().Where(go =>
                     {
                         return go.name.Contains("Radiant Orb") ||
                                go.name.Contains("Radiant Spike") ||
                                go.name.Contains("Radiant Nail") ||
                                go.name.Contains("Radiant Beam") ||
                                go.name.Contains("Beam Orb");
                     }))
            {
                Destroy(attack);
            }
            AnyRadiance.Instance.GameObjects["Legs"].SetActive(false);
            _collider.enabled = false;
            _animator.Play("Stun");
            AnyRadiance.Instance.Particles["Feather Burst"].Stop();
            AnyRadiance.Instance.GameObjects["White Flash"].SetActive(true);

            yield return new WaitForSeconds(2);

            GameObject roarWaveStun = AnyRadiance.Instance.GameObjects["Roar Wave Stun"];
            GameObject stunEyeGlow = AnyRadiance.Instance.GameObjects["Stun Eye Glow"];
            stunEyeGlow.LocateMyFSM("FSM").SendEvent("UP");
            AnyRadiance.Instance.AudioClips["Tentacle Sucking"].PlayOneShot(transform.position);
            roarWaveStun.SetActive(true);
            shakeFSM.GetFsmBool("RumblingBig").Value = true;

            yield return new WaitForSeconds(1.5f);

            roarWaveStun.SetActive(false);
            AnyRadiance.Instance.AudioClips["Burst Move Up"].PlayOneShot(transform.position);
            _renderer.enabled = false;
            shakeFSM.GetFsmBool("RumblingBig").Value = false;
            stunEyeGlow.LocateMyFSM("FSM").SendEvent("DOWN INSTANT");
            AnyRadiance.Instance.GameObjects["White Flash"].SetActive(true);
            CamShake("BigShake");
            AnyRadiance.Instance.Particles["Stun Out Burst"].Play();
            AnyRadiance.Instance.Particles["Stun Out Rise"].Play();

            yield return new WaitForSeconds(2);

            AnyRadiance.Instance.GameObjects["CamLock A1"].SetActive(false);
            foreach (var fsm in AnyRadiance.Instance.GameObjects["Plat Sets"].GetComponentsInChildren<PlayMakerFSM>())
            {
                if (fsm.transform.parent.name == "Ascend Set") continue;
                fsm.SendEvent("APPEAR");
                fsm.GetComponent<SpikedPlatform>()?.UpInstant();
            }

            iTween.MoveBy(AnyRadiance.Instance.GameObjects["Abyss Pit"], Vector3.up, 1);

            yield return new WaitForSeconds(2);

            iTween.MoveTo(AnyRadiance.Instance.GameObjects["Abyss Pit"], new Vector3(61.77f, 30, 0), 4);

            AnyRadiance.Instance.GameObjects["Plat Sets"].Child("Climb Set/Radiant Plat Wide (2)")
                .LocateMyFSM("radiant_plat").SendEvent("DISAPPEAR");
            AnyRadiance.Instance.GameObjects["CamLock A1"].SetActive(false);
            AnyRadiance.Instance.GameObjects["CamLock Main"].SetActive(false);
            AnyRadiance.Instance.GameObjects["CamLock A2"].SetActive(true);
            shakeFSM.GetFsmBool("RumblingMed").Value = false;

            var portals = Instantiate(AnyRadiance.Instance.GameObjects["Portals"]);
            var portalManager = portals.AddComponent<PortalManager>();
            foreach (var spikedPlat in FindObjectsOfType<SpikedPlatform>())
            {
                foreach (Transform child in spikedPlat.transform)
                {
                    if (child.name.Contains("Plat Spike"))
                    {
                        portalManager.ExcludeTeleport(child.gameObject);
                    }
                }
            }

            yield return new WaitForSeconds(1);

            _logic = StartCoroutine(StartPhase2());
        }

        private IEnumerator Phase2Death()
        {
            yield return null;
            StopCoroutine(_logic);
            var shakeFSM = GameCameras.instance.cameraShakeFSM.Fsm;
            GameManager.instance.gameObject.Child("GlobalPool/Stun Effect(Clone)").Spawn(transform.position);
            AnyRadiance.Instance.AudioClips["Knock Down"].PlayOneShot(transform.position);
            shakeFSM.GetFsmBool("RumblingMed").Value = false;
            CamShake("BigShake");
            _haloFSM.SendEvent("DOWN");
            foreach (var attack in FindObjectsOfType<GameObject>().Where(go => go.name.Contains("Radiant Orb") ||
                                                                               go.name.Contains("Portals") ||
                                                                               go.name.Contains("Radiant Spike") ||
                                                                               go.name.Contains("Radiant Nail") ||
                                                                               go.name.Contains("Radiant Beam")))
            {
                Destroy(attack);
            }
            AnyRadiance.Instance.GameObjects["Legs"].SetActive(false);
            _collider.enabled = false;
            _animator.Play("Death");
            AnyRadiance.Instance.Particles["Feather Burst"].Stop();
            AnyRadiance.Instance.GameObjects["White Flash"].SetActive(true);

            yield return new WaitForSeconds(2);

            GameObject roarWaveStun = AnyRadiance.Instance.GameObjects["Roar Wave Stun"];
            GameObject stunEyeGlow = AnyRadiance.Instance.GameObjects["Stun Eye Glow"];
            roarWaveStun.transform.localPosition = stunEyeGlow.transform.localPosition = new Vector2(-0.5f, 3);
            stunEyeGlow.LocateMyFSM("FSM").SendEvent("UP");
            AnyRadiance.Instance.AudioClips["Tentacle Sucking"].PlayOneShot(transform.position);
            roarWaveStun.SetActive(true);
            shakeFSM.GetFsmBool("RumblingBig").Value = true;

            yield return new WaitForSeconds(1.5f);

            roarWaveStun.SetActive(false);
            AnyRadiance.Instance.AudioClips["Burst Move Up"].PlayOneShot(transform.position);
            _renderer.enabled = false;
            shakeFSM.GetFsmBool("RumblingBig").Value = false;
            AnyRadiance.Instance.GameObjects["Stun Eye Glow"].SetActive(false);
            AnyRadiance.Instance.GameObjects["White Flash"].SetActive(true);
            CamShake("BigShake");
            AnyRadiance.Instance.Particles["Stun Out Burst"].Play();
            AnyRadiance.Instance.Particles["Stun Out Rise"].Play();

            yield return new WaitForSeconds(2);

            shakeFSM.GetFsmBool("RumblingMed").Value = false;
            shakeFSM.GetFsmBool("RumblingBig").Value = false;

            yield return new WaitForSeconds(1);

            AnyRadiance.Instance.GameObjects["CamLock A2"].SetActive(false);
            AnyRadiance.Instance.GameObjects["CamLocks Ascend"].SetActive(true);
            foreach (var fsm in AnyRadiance.Instance.GameObjects["Plat Sets"].GetComponentsInChildren<PlayMakerFSM>())
            {
                foreach (var spikeFSM in fsm.GetComponentsInChildren<PlayMakerFSM>()
                             .Where(pfsm => pfsm.name.Contains("Moving Plat Spike")))
                {
                    spikeFSM.SendEvent("UP");
                }
            }

            _logic = StartCoroutine(StartPhase3());
        }

        private IEnumerator Phase3Death()
        {
            var shakeFSM = GameCameras.instance.cameraShakeFSM.Fsm;
            GameManager.instance.gameObject.Child("GlobalPool/Stun Effect(Clone)").Spawn(transform.position);
            AnyRadiance.Instance.AudioClips["Knock Down"].PlayOneShot(transform.position);
            shakeFSM.GetFsmBool("RumblingMed").Value = true;
            CamShake("BigShake");
            _haloFSM.SendEvent("DOWN");
            foreach (var attack in FindObjectsOfType<GameObject>().Where(go => go.name.Contains("Nail") || go.name.Contains("Orb")))
            {
                Destroy(attack);
            }
            AnyRadiance.Instance.GameObjects["Legs"].SetActive(false);
            _collider.enabled = false;
            _animator.Play("Death");
            AnyRadiance.Instance.Particles["Feather Burst"].Stop();
            AnyRadiance.Instance.GameObjects["White Flash"].SetActive(true);

            AnyRadiance.Instance.GameObjects["CamLock Death1"].SetActive(true);
            GameCameras.instance.hudCanvas.LocateMyFSM("Slide Out").SendEvent("OUT");
            PlayMakerFSM.BroadcastEvent("ALL CHARMS END");
            GameManager.instance.GetComponentInChildren<AudioManager>().ApplyMusicCue(null, 0, 0, false);

            var hc = HeroController.instance;
            foreach (var fsm in hc.GetComponentsInChildren<PlayMakerFSM>())
            {
                fsm.SendEvent("FSM CANCEL");
            }

            hc.RelinquishControl();
            hc.StopAnimationControl();
            hc.AffectedByGravity(false);
            PlayerData.instance.isInvincible = true;
            hc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            hc.GetComponent<MeshRenderer>().enabled = false;
            var knightSplit = gameObject.Child("Death/Knight Split");
            knightSplit.transform.position = hc.transform.position;
            iTween.MoveTo(knightSplit, iTween.Hash("position", new Vector3(64.49f, 145.32f, 0.002f), "time", 0.25f, "easetype", iTween.EaseType.easeOutSine));
            var splitAnim = knightSplit.GetComponent<tk2dSpriteAnimator>();
            splitAnim.Play("Knight Split Antic");
            
            yield return new WaitForSeconds(1);

            splitAnim.Play("Knight Split");
            AnyRadiance.Instance.AudioClips["Explode"].PlayOneShot(hc.transform.position);
            knightSplit.LocateMyFSM("Split").SendEvent("SPLIT");
            var knightBall = knightSplit.Child("Knight Ball");
            knightBall.SetActive(true);
            knightBall.transform.SetParent(null);
            CamShake("AverageShake");

            yield return new WaitForSeconds(0.75f);

            iTween.MoveTo(AnyRadiance.Instance.GameObjects["Abyss Pit"], new Vector2(56.5f, 152.38f), 1);

            yield return new WaitForSeconds(1.5f);

            AnyRadiance.Instance.AudioClips["Rumble"].PlayOneShot(hc.transform.position);
            AnyRadiance.Instance.AudioClips["Final Hit 2"].PlayOneShot(hc.transform.position);
            CamShake("HugeShake");
            AnyRadiance.Instance.Particles["Death Stream"].Play();
            
            yield return new WaitForSeconds(3);

            shakeFSM.GetFsmBool("RumblingMed").Value = false;
            shakeFSM.GetFsmBool("RumblingBig").Value = false;
            AnyRadiance.Instance.AudioClips["Final Hit 3"].PlayOneShot(hc.transform.position);
            AnyRadiance.Instance.GameObjects["Final Explode Statue"].SetActive(true);
            AnyRadiance.Instance.GameObjects["Statue Death Fader"].SetActive(true);
            AnyRadiance.Instance.Particles["Death Stream"].Stop();

            yield return new WaitForSeconds(4.5f);
                
            var dialogueText = GameCameras.instance.hudCamera.transform.Find("DialogueManager/Text");
            var tmp = dialogueText.GetComponent<TextMeshPro>();
            tmp.color = Color.black;
            tmp.alignment = TextAlignmentOptions.Center;
            dialogueText.GetComponent<DialogueBox>()
                .StartConversation("GODSEEKER_ANYRAD_STATUE", "CP3");

            yield return new WaitForSeconds(3);

            tmp.color = Color.white;
            BossSceneController.Instance.EndBossScene();
            PlayerData.instance.isInvincible = false;
        }

        private void GetAudioClips()
        {
            PlayMakerFSM cmdFSM = gameObject.LocateMyFSM("Attack Commands");
            PlayMakerFSM ctrlFSM = gameObject.LocateMyFSM("Control");
            PlayMakerFSM teleFSM = gameObject.LocateMyFSM("Teleport");

            AnyRadiance.Instance.GameObjects["Audio Player"] = teleFSM.GetAction<AudioPlayerOneShotSingle>("Antic").audioPlayer.Value;
            AnyRadiance.Instance.GameObjects["Nail"] = cmdFSM.GetAction<SpawnObjectFromGlobalPool>("CW Spawn").gameObject.Value;
            AnyRadiance.Instance.GameObjects["Orb"] = cmdFSM.GetAction<SpawnObjectFromGlobalPool>("Spawn Fireball").gameObject.Value;
            AnyRadiance.Instance.AudioClips["Appear"] = (AudioClip)ctrlFSM.GetAction<AudioPlayerOneShotSingle>("First Tele").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Beam Burst"] = (AudioClip)cmdFSM.GetAction<AudioPlayerOneShotSingle>("EB 1").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Beam Prepare"] = (AudioClip)cmdFSM.GetAction<AudioPlaySimple>("EB 1").oneShotClip.Value;
            AnyRadiance.Instance.AudioClips["Burst Move Up"] = (AudioClip)ctrlFSM.GetAction<AudioPlayerOneShotSingle>("Stun1 Out").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Explode"] = (AudioClip)ctrlFSM.GetAction<AudioPlayerOneShotSingle>("Knight Break").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Final Hit 2"] = (AudioClip)ctrlFSM.GetAction<AudioPlayerOneShotSingle>("Statue Death 1").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Final Hit 3"] = (AudioClip)ctrlFSM.GetAction<AudioPlayerOneShotSingle>("Statue Death 2").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Ghost"] = (AudioClip)cmdFSM.GetAction<AudioPlayerOneShotSingle>("Orb Summon").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Knock Down"] = (AudioClip)ctrlFSM.GetAction<AudioPlayerOneShotSingle>("Stun1 Start").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Projectile"] = (AudioClip)cmdFSM.GetAction<AudioPlaySimple>("Spawn Fireball").oneShotClip.Value;
            AnyRadiance.Instance.AudioClips["Rumble"] = (AudioClip)ctrlFSM.GetAction<AudioPlayerOneShotSingle>("Statue Death 1").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Scream"] = (AudioClip)ctrlFSM.GetAction<AudioPlayerOneShotSingle>("Scream").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Sword Create"] = (AudioClip)cmdFSM.GetAction<AudioPlayerOneShotSingle>("Dir").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Sword Shoot"] = (AudioClip)cmdFSM.GetAction<AudioPlayerOneShotSingle>("CW Fire").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Tele"] = (AudioClip)teleFSM.GetAction<AudioPlayerOneShotSingle>("Antic").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Tentacle Sucking"] = (AudioClip)ctrlFSM.GetAction<AudioPlayerOneShotSingle>("Stun1 Roar").audioClip.Value;
        }

        private void GetChildren()
        {
            _bossCtrl = transform.parent.gameObject;

            AnyRadiance.Instance.GameObjects["CamLock A1"] = _bossCtrl.Child("CamLocks/CamLock A1");
            AnyRadiance.Instance.GameObjects["CamLock A2"] = _bossCtrl.Child("CamLocks/CamLock A2");
            AnyRadiance.Instance.GameObjects["CamLock Death1"] = _bossCtrl.Child("CamLocks/CamLock Death1");
            AnyRadiance.Instance.GameObjects["CamLock Main"] = _bossCtrl.Child("CamLocks/CamLock Main");
            AnyRadiance.Instance.GameObjects["CamLocks Ascend"] = _bossCtrl.Child("CamLocks/CamLocks Ascend");
            AnyRadiance.Instance.GameObjects["Plat Sets"] = _bossCtrl.Child("Plat Sets");
            AnyRadiance.Instance.GameObjects["Abyss Pit"] = _bossCtrl.Child("Abyss Pit");
            AnyRadiance.Instance.GameObjects["White Fader"] = _bossCtrl.Child("White Fader");
            AnyRadiance.Instance.GameObjects["Spike"] = _bossCtrl.Child("Spike Control/Far L/Radiant Spike");
            AnyRadiance.Instance.GameObjects["Final Explode Statue"] = _bossCtrl.Child("Final Explode Statue");
            AnyRadiance.Instance.GameObjects["Statue Death Fader"] = _bossCtrl.Child("Statue Death Fader");

            AnyRadiance.Instance.GameObjects["Glow"] = gameObject.Child("Eye Beam Glow");
            AnyRadiance.Instance.GameObjects["Ascend Beam"] = gameObject.Child("Eye Beam Glow/Ascend Beam");
            AnyRadiance.Instance.GameObjects["Eye Beam"] = gameObject.Child("Eye Beam Glow/Burst 1/Radiant Beam");
            _haloFSM = gameObject.Child("Halo").LocateMyFSM("Fader");
            AnyRadiance.Instance.GameObjects["Legs"] = gameObject.Child("Legs");
            AnyRadiance.Instance.GameObjects["Roar Wave Stun"] = gameObject.Child("Roar Wave Stun");
            AnyRadiance.Instance.GameObjects["Shot Charge"] = gameObject.Child("Shot Charge");
            AnyRadiance.Instance.GameObjects["Stun Eye Glow"] = gameObject.Child("Stun Eye Glow");
            AnyRadiance.Instance.GameObjects["Tele Flash"] = gameObject.Child("Tele Flash");
            AnyRadiance.Instance.GameObjects["White Flash"] = gameObject.Child("White Flash");

            AnyRadiance.Instance.Particles["Death Stream"] = gameObject.Child("Death Stream Pt").GetComponent<ParticleSystem>();
            AnyRadiance.Instance.Particles["Feather Burst"] = gameObject.Child("Pt Feather Burst").GetComponent<ParticleSystem>();
            AnyRadiance.Instance.Particles["Stun Out Burst"] = gameObject.Child("Pt StunOutBurst").GetComponent<ParticleSystem>();
            AnyRadiance.Instance.Particles["Stun Out Rise"] = gameObject.Child("Pt StunOutRise").GetComponent<ParticleSystem>();
            AnyRadiance.Instance.Particles["Tele Out"] = gameObject.Child("Pt Tele Out").GetComponent<ParticleSystem>();

            AnyRadiance.Instance.GameObjects["Eye Beam"].CreatePool(30);
            AnyRadiance.Instance.GameObjects["Spike"].CreatePool(50);
            AnyRadiance.Instance.GameObjects["Abyss Pit"].GetComponentInChildren<DamageHero>().damageDealt = 2;

            var platSets = AnyRadiance.Instance.GameObjects["Plat Sets"];
            foreach (Transform child in platSets.transform)
            {
                if (child.name == "Climb Set" || child.name == "P2 SetA") continue;
                foreach (Transform grandChild in child)
                {
                    Destroy(grandChild.gameObject);
                }
            }

            foreach (Transform child in AnyRadiance.Instance.GameObjects["Plat Sets"].transform)
            {
                if (child.name == "P2 SetA")
                {
                    child.gameObject.SetActive(false);
                }
            }

            // Create a circle of rotating platforms
            GameObject a2Plats = new GameObject("A2 Plats");
            a2Plats.transform.SetParent(platSets.transform);
            var rotator = a2Plats.AddComponent<PlatformRotator>();
            var platPrefab = platSets.Child("P2 SetA/Radiant Plat Small (2)");
            for (int i = 0; i < NumPlatsA2; i++)
            {
                var plat = Instantiate(platPrefab, a2Plats.transform);
                plat.AddComponent<MovingPlatform>();
                plat.AddComponent<SpikedPlatform>();
                rotator.AddPlatform(plat);
            }

            // Create randomized ascend platforms
            var ascendSet = platSets.Child("Ascend Set");
            foreach (Transform child in ascendSet.transform)
            {
                Destroy(child.gameObject);
            }

            float prevX = 0;
            for (int i = 0; i < NumPlatsAscend; i++)
            {
                var plat = Instantiate(platPrefab, ascendSet.transform);
                var diff = ArenaInfo.A3Top - ArenaInfo.A3Bottom;
                var x = Random.Range(ArenaInfo.A3Left + 1, ArenaInfo.A3Right - 1);
                while (Mathf.Abs(prevX - x) < 3)
                {
                    x = Random.Range(ArenaInfo.A3Left + 1, ArenaInfo.A3Right - 1);
                }

                prevX = x;
                plat.transform.SetPosition2D(x, ArenaInfo.A3Bottom + diff * i / NumPlatsAscend);
                plat.AddComponent<EtherealPlatform>();
                plat.AddComponent<SpikedPlatform>();
            }

            ascendSet.SetActive(false);

            var finalHazardPlat = Instantiate(platPrefab);
            finalHazardPlat.name = "Final Hazard Plat";
            finalHazardPlat.transform.SetPosition2D(55, 155);
            finalHazardPlat.SetActive(false);
        }

        #region Helpers

        private void CamShake(string eventName)
        {
            GameCameras.instance.cameraShakeFSM.SendEvent(eventName);
        }
        #endregion

        #region Teleport
        private IEnumerator TeleIn(Vector3 destination)
        {
            AnyRadiance.Instance.GameObjects["Tele Flash"].SetActive(true);
            yield return new WaitForSeconds(0.11f);

            transform.SetPosition2D(destination);
            _collider.enabled = true;
            AnyRadiance.Instance.GameObjects["Legs"].SetActive(true);
            _renderer.enabled = true;
            AnyRadiance.Instance.Particles["Feather Burst"].Play();
            CamShake("EnemyKillShake");
            _haloFSM.SendEvent("UP");
            yield return _animator.PlayUntilFinished("Tele In");
        }

        private IEnumerator TeleOut()
        {
            _collider.enabled = false;
            AnyRadiance.Instance.GameObjects["Legs"].SetActive(false);
            _rigidbody.velocity = Vector2.zero;
            AnyRadiance.Instance.Particles["Tele Out"].Play();
            AnyRadiance.Instance.AudioClips["Tele"].PlayOneShot(transform.position);
            yield return _animator.PlayUntilFinished("Tele Out");
            _renderer.enabled = false;
        }

        #endregion

        private IEnumerator StartPhase1()
        {
            Phase++;
            On.HealthManager.Die += StartPhase1Death;

            AnyRadiance.Instance.AudioClips["Appear"].PlayOneShot(transform.position);
            GameCameras.instance.cameraShakeFSM.SendEvent("BigShake");
            yield return TeleOut();
            yield return TeleIn(new Vector3(60.63f, 28.3f, 0.006f));

            // Activate all spikes and make them stay up.
            foreach (var fsm in FindObjectsOfType<PlayMakerFSM>(true)
                         .Where(fsm => fsm.name.Contains("Radiant Spike")))
            {
                if (fsm.FsmName == "Control")
                {
                    fsm.GetComponent<DamageHero>().damageDealt = 2;
                    fsm.SendEvent("UP");
                }
                else if (fsm.FsmName == "Hero Saver")
                {
                    // Remove behavior of spikes retracting after player touches them
                    Destroy(fsm);
                }
            }

            while (_healthManager.hp >= 0)
            {
                _animator.Play("Recover");
                yield return new WaitForSeconds(Random.Range(0.25f, 0.35f));
                yield return _animator.PlayUntilFinished("Cast");
                yield return ChooseAttackP1();

                yield return TeleOut();
                yield return TeleIn(new Vector3(Random.Range(ArenaInfo.CurrentLeft, ArenaInfo.CurrentRight), transform.position.y, transform.position.z));
            }
        }

        private IEnumerator StartPhase2()
        {
            Phase++;
            On.HealthManager.Die -= StartPhase1Death;
            On.HealthManager.Die += StartPhase2Death;
            _healthManager.IsInvincible = true;

            AnyRadiance.Instance.AudioClips["Appear"].PlayOneShot(transform.position);
            GameCameras.instance.cameraShakeFSM.SendEvent("BigShake");
            yield return TeleIn(new Vector3(Random.Range(ArenaInfo.CurrentLeft, ArenaInfo.CurrentRight), Random.Range(ArenaInfo.CurrentBottom, ArenaInfo.CurrentTop), transform.position.z));

            yield return _summonBeamOrbP2.Execute();

            _healthManager.hp = Phase2HP;
            while (_healthManager.hp > MegaOrbHP)
            {
                _animator.Play("Recover");
                yield return new WaitForSeconds(Random.Range(0.25f, 0.35f));
                yield return _animator.PlayUntilFinished("Cast");
                yield return ChooseAttackP2();

                yield return TeleOut();
                yield return TeleIn(new Vector3(Random.Range(ArenaInfo.CurrentLeft, ArenaInfo.CurrentRight), Random.Range(ArenaInfo.CurrentBottom, ArenaInfo.CurrentTop), transform.position.z));
            }

            yield return TeleOut();
            yield return TeleIn(new Vector3(ArenaInfo.CurrentCenterX, ArenaInfo.CurrentBottom + (ArenaInfo.CurrentTop - ArenaInfo.CurrentBottom) / 2, 0));

            yield return _summonMegaOrb.Execute();
        }

        private IEnumerator StartPhase3()
        {
            Phase++;
            On.HealthManager.Die -= StartPhase2Death;
            On.HealthManager.Die += StartPhase3Death;
            _animator.Play("Cast");
            _healthManager.hp = Phase3HP;
            _healthManager.IsInvincible = false;

            _collider.enabled = true;
            _renderer.enabled = true;
            transform.SetPosition2D(62.94f, 158.65f);

            var ascendSet = AnyRadiance.Instance.GameObjects["Plat Sets"].Child("Ascend Set");
            ascendSet.SetActive(true);
            ascendSet.transform.GetChild(0).GetComponent<EtherealPlatform>().Appear();

            var beamOrb = GameObject.Find("Beam Orb Bouncer");
            Destroy(beamOrb.GetComponent<Bouncer>());
            beamOrb.AddComponent<BackAndForthFollow>();

            yield return new WaitUntil(() => ascendSet.transform.GetChild(1).GetComponent<EtherealPlatform>().Appeared);

            FindObjectOfType<PlatformRotator>().RemoveHazardPoint();

            yield return new WaitUntil(() => HeroController.instance.transform.position.y >= FinalPhaseY);

            AnyRadiance.Instance.GameObjects["Abyss Pit"].GetComponentInChildren<DamageHero>().damageDealt = int.MaxValue;
            AnyRadiance.Instance.AudioClips["Scream"].PlayOneShot(transform.position);
            var teleInClip = _animator.Library.GetClipByName("Tele Out");
            teleInClip.wrapMode = tk2dSpriteAnimationClip.WrapMode.LoopSection;
            teleInClip.loopStart = 2;
            _animator.Play("Tele Out");
            StartCoroutine(beamOrb.GetComponent<BeamOrb>().Dissipate(0));

            StartCoroutine(_summonTrailNail.Execute());
            yield return new WaitUntil(() => _healthManager.hp <= Phase3TrailNail2HP);
            StartCoroutine(_summonTrailNail.Execute());
            yield return new WaitUntil(() => _healthManager.hp <= Phase3TrailNail3HP);
            StartCoroutine(_summonTrailNail.Execute());
        }
        
        private IEnumerator ChooseAttackP1()
        {
            yield return _phase1Subphase1Actions.RandomSequence().Execute();
            if (_healthManager.hp <= Phase1HP / 2)
            {
                // Perform another attack simultaneously
                StartCoroutine(_phase1Subphase2Actions.RandomSequence().Execute());
            }
        }

        private IEnumerator ChooseAttackP2()
        {
            yield return _phase2Actions.RandomSequence().Execute();
        }
    }
}