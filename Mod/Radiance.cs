using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;
using Modding.Utils;
using System.Collections;
using System.Linq;
using UnityEngine;
using Vasi;

namespace AnyRadiance
{
    internal partial class Radiance : MonoBehaviour
    {
        #region Constants

        private const int NumPlatsA2 = 7;
#if DEBUG
        private const int Phase1HP = 100;
        private const int Phase2HP = 100;
        private const int UltraOrbHP = 51;
#else
        private const int Phase1HP = 2000;
        private const int Phase2HP = 1200;
        private const int UltraOrbHP = 250;
#endif
        #endregion

        #region Components

        private tk2dSpriteAnimator _animator;
        private AudioSource _audio;
        private PolygonCollider2D _collider;
        private HealthManager _healthManager;
        private MeshRenderer _renderer;
        private Rigidbody2D _rigidbody;
        #endregion

        #region Other Game Objects and Components

        private GameObject _bossCtrl;
        private PlayMakerFSM _haloFSM;
        private HeroController _hc;
        private PlayerData _pd;
        private PlayMakerFSM _spellCtrl;
        #endregion

        private static Coroutine _logic;

        private void Awake()
        {
            _animator = GetComponent<tk2dSpriteAnimator>();
            _audio = GetComponent<AudioSource>();
            _collider = GetComponent<PolygonCollider2D>();
            _healthManager = GetComponent<HealthManager>();
            _renderer = GetComponent<MeshRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();

            _healthManager.hp = Phase1HP;

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
            AnyRadiance.Instance.AudioClips["Final Hit"] = (AudioClip)ctrlFSM.GetAction<AudioPlayerOneShotSingle>("Statue Death 2").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Ghost"] = (AudioClip)cmdFSM.GetAction<AudioPlayerOneShotSingle>("Orb Summon").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Knock Down"] = (AudioClip)ctrlFSM.GetAction<AudioPlayerOneShotSingle>("Stun1 Start").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Projectile"] = (AudioClip)cmdFSM.GetAction<AudioPlaySimple>("Spawn Fireball").oneShotClip.Value;
            AnyRadiance.Instance.AudioClips["Sword Create"] = (AudioClip)cmdFSM.GetAction<AudioPlayerOneShotSingle>("Dir").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Sword Shoot"] = (AudioClip)cmdFSM.GetAction<AudioPlayerOneShotSingle>("CW Fire").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Tele"] = (AudioClip)teleFSM.GetAction<AudioPlayerOneShotSingle>("Antic").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Tentacle Sucking"] = (AudioClip)ctrlFSM.GetAction<AudioPlayerOneShotSingle>("Stun1 Roar").audioClip.Value;

            _hc = HeroController.instance;
            _pd = PlayerData.instance;

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

            var knight = HeroController.instance.gameObject.GetComponent<Knight>();
            if (knight != null) Destroy(knight);

            On.HealthManager.Die -= StartPhase1Death;
            On.HealthManager.Die -= StartPhase2Death;
        }

        private void StartPhase1Death(On.HealthManager.orig_Die orig, HealthManager self, float? attackDirection, AttackTypes attackType, bool ignoreEvasion)
        {
            orig(self, attackDirection, attackType, ignoreEvasion);
            if (self.name != "Absolute Radiance") return;
            StartCoroutine(Phase1Death());
        }

        private void StartPhase2Death(On.HealthManager.orig_Die orig, HealthManager self, float? attackDirection, AttackTypes attackType, bool ignoreEvasion)
        {
            orig(self, attackDirection, attackType, ignoreEvasion);
            if (self.name != "Absolute Radiance") return;
            StartCoroutine(Phase2Death());
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
                                go.name.Contains("Ascend Beam");
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
            foreach (var attack in FindObjectsOfType<GameObject>().Where(go =>
                     {
                         return go.name.Contains("Radiant Orb") ||
                                go.name.Contains("Radiant Spike") ||
                                go.name.Contains("Radiant Nail") ||
                                go.name.Contains("Ascend Beam");
                     }))
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

            shakeFSM.GetFsmBool("RumblingBig").Value = false;
        }

        private void GetChildren()
        {
            _bossCtrl = transform.parent.gameObject;

            AnyRadiance.Instance.GameObjects["CamLock A1"] = _bossCtrl.Child("CamLocks/CamLock A1");
            AnyRadiance.Instance.GameObjects["CamLock A2"] = _bossCtrl.Child("CamLocks/CamLock A2");
            AnyRadiance.Instance.GameObjects["CamLock Main"] = _bossCtrl.Child("CamLocks/CamLock Main");
            AnyRadiance.Instance.GameObjects["Plat Sets"] = _bossCtrl.Child("Plat Sets");
            AnyRadiance.Instance.GameObjects["Abyss Pit"] = _bossCtrl.Child("Abyss Pit");
            AnyRadiance.Instance.GameObjects["White Fader"] = _bossCtrl.Child("White Fader");
            
            AnyRadiance.Instance.GameObjects["Glow"] = gameObject.Child("Eye Beam Glow");
            AnyRadiance.Instance.GameObjects["Beam"] = gameObject.Child("Eye Beam Glow/Ascend Beam");
            AnyRadiance.Instance.GameObjects["Eye Beam"] = gameObject.Child("Eye Beam Glow/Burst 1/Radiant Beam");
            _haloFSM = gameObject.Child("Halo").LocateMyFSM("Fader");
            AnyRadiance.Instance.GameObjects["Legs"] = gameObject.Child("Legs");
            AnyRadiance.Instance.GameObjects["Roar Wave Stun"] = gameObject.Child("Roar Wave Stun");
            AnyRadiance.Instance.GameObjects["Shot Charge"] = gameObject.Child("Shot Charge");
            AnyRadiance.Instance.GameObjects["Stun Eye Glow"] = gameObject.Child("Stun Eye Glow");
            AnyRadiance.Instance.GameObjects["Tele Flash"] = gameObject.Child("Tele Flash");
            AnyRadiance.Instance.GameObjects["White Flash"] = gameObject.Child("White Flash");

            AnyRadiance.Instance.Particles["Feather Burst"] = gameObject.Child("Pt Feather Burst").GetComponent<ParticleSystem>();
            AnyRadiance.Instance.Particles["Tele Out"] = gameObject.Child("Pt Tele Out").GetComponent<ParticleSystem>();
            AnyRadiance.Instance.Particles["Stun Out Burst"] = gameObject.Child("Pt StunOutBurst").GetComponent<ParticleSystem>();
            AnyRadiance.Instance.Particles["Stun Out Rise"] = gameObject.Child("Pt StunOutRise").GetComponent<ParticleSystem>();

            AnyRadiance.Instance.GameObjects["Beam"].CreatePool(30);
            AnyRadiance.Instance.GameObjects["Abyss Pit"].GetComponentInChildren<DamageHero>().damageDealt = 2;

            GameObject a2Plats = new GameObject("A2 Plats");
            a2Plats.transform.SetParent(AnyRadiance.Instance.GameObjects["Plat Sets"].transform);
            var rotator = a2Plats.AddComponent<PlatformRotator>();
            var platPrefab = AnyRadiance.Instance.GameObjects["Plat Sets"].Child("P2 SetA/Radiant Plat Small (2)");
            for (int i = 0; i < NumPlatsA2; i++)
            {
                var plat = Instantiate(platPrefab, a2Plats.transform);
                plat.AddComponent<MovingPlatform>();
                rotator.AddPlatform(plat);
            }
            foreach (Transform child in AnyRadiance.Instance.GameObjects["Plat Sets"].transform)
            {
                if (child.name == "A2 Plats" || child.name == "Climb Set") continue;
                Destroy(child.gameObject);
            }
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
            ArenaInfo.SetPhase(1);
            On.HealthManager.Die += StartPhase1Death;

            AnyRadiance.Instance.AudioClips["Appear"].PlayOneShot(transform.position);
            GameCameras.instance.cameraShakeFSM.SendEvent("BigShake");
            yield return TeleOut();
            yield return TeleIn(new Vector3(60.63f, 27, 0.006f));

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
            ArenaInfo.SetPhase(2);
            On.HealthManager.Die -= StartPhase1Death;
            On.HealthManager.Die += StartPhase2Death;
            _healthManager.IsInvincible = true;

            AnyRadiance.Instance.AudioClips["Appear"].PlayOneShot(transform.position);
            GameCameras.instance.cameraShakeFSM.SendEvent("BigShake");
            yield return TeleIn(new Vector3(Random.Range(ArenaInfo.CurrentLeft, ArenaInfo.CurrentRight), Random.Range(ArenaInfo.CurrentBottom, ArenaInfo.CurrentTop), transform.position.z));

            yield return SummonBeamOrb(
                true,
                30,
                45,
                0.1f,
                6,
                5,
                1,
                float.MaxValue,
                true);

            _healthManager.hp = Phase1HP;
            while (_healthManager.hp >= UltraOrbHP)
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

            yield return SummonUltraOrb(5, 0.2f);
        }

        private IEnumerator ChooseAttackP1()
        {
            var attackOptions = new[] { LaserColumns(), NailWalls() };
            var extraAttacks = new[]
            {
                NailBarrage(), 
                SummonBeamOrb(
                    false, 
                    0, 
                    15, 
                    0.25f, 
                    3, 
                    3, 
                    0.5f, 
                    2), 
                SummonMegaOrb(
                    2.0f / 3, 
                    0.5f, 
                    1.5f, 
                    12, 
                    2, 
                    2, 
                    1.5f, 
                    2),
            };
            var chosenAttack = attackOptions[Random.Range(0, attackOptions.Length)];
            if (_healthManager.hp <= Phase1HP / 2)
            {
                // Perform another attack simultaneously
                var extraAttack = extraAttacks[Random.Range(0, extraAttacks.Length)];
                StartCoroutine(extraAttack);
            }
            yield return chosenAttack;
        }

        private IEnumerator ChooseAttackP2()
        {
            var attackOptions = new[] { 
                NailBarrage(),
                SummonMegaOrb(
                    1, 
                    0.5f, 
                    2, 
                    12, 
                    2, 
                    2, 
                    2, 
                    2)
            };
            var chosenAttack = attackOptions[Random.Range(0, attackOptions.Length)];
            yield return chosenAttack;
        }
    }
}