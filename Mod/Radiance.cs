using GlobalEnums;
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
#if DEBUG
        private const int Phase1HP = 100;
#else
        private const int Phase1HP = 2000;
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

            On.HealthManager.Die += StartDeath;
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.4f);
            PlayOneShot(AnyRadiance.Instance.AudioClips["Appear"], transform.position);
            GameCameras.instance.cameraShakeFSM.SendEvent("BigShake");
            yield return StartCoroutine(TeleOut());
            yield return StartCoroutine(TeleIn(new Vector3(60.63f, 27, 0.006f)));
            _logic = StartCoroutine(StartBattle());
        }

        private void OnDestroy()
        {
            var quake1Down = _spellCtrl.GetAction<CallMethodProper>("Quake1 Down");
            var quake2Down = _spellCtrl.GetAction<CallMethodProper>("Quake2 Down");
            ReflectionHelper.SetField<FsmStateAction, bool>(quake1Down, "enabled", true);
            ReflectionHelper.SetField<FsmStateAction, bool>(quake2Down, "enabled", true);

            var knight = HeroController.instance.gameObject.GetComponent<Knight>();
            if (knight != null) Destroy(knight);

            On.HealthManager.Die -= StartDeath;
        }

        private void StartDeath(On.HealthManager.orig_Die orig, HealthManager self, float? attackDirection, AttackTypes attackType, bool ignoreEvasion)
        {
            orig(self, attackDirection, attackType, ignoreEvasion);
            if (self.name != "Absolute Radiance") return;
            StartCoroutine(Death());
        }

        private IEnumerator Death()
        {
            StopCoroutine(_logic);
            var shakeFSM = GameCameras.instance.cameraShakeFSM.Fsm;
            GameManager.instance.gameObject.Child("GlobalPool/Stun Effect(Clone)").Spawn(transform.position);
            PlayOneShot(AnyRadiance.Instance.AudioClips["Knock Down"], transform.position);
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

            AnyRadiance.Instance.GameObjects["Stun Eye Glow"].LocateMyFSM("FSM").SendEvent("UP");
            PlayOneShot(AnyRadiance.Instance.AudioClips["Tentacle Sucking"], transform.position);
            AnyRadiance.Instance.GameObjects["Roar Wave Stun"].SetActive(true);
            shakeFSM.GetFsmBool("RumblingBig").Value = true;

            yield return new WaitForSeconds(1.5f);

            AnyRadiance.Instance.GameObjects["Roar Wave Stun"].SetActive(false);
            PlayOneShot(AnyRadiance.Instance.AudioClips["Burst Move Up"], transform.position);
            _renderer.enabled = false;
            shakeFSM.GetFsmBool("RumblingBig").Value = false;
            AnyRadiance.Instance.GameObjects["Stun Eye Glow"].SetActive(false);
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

            HeroController.instance.SetHazardRespawn(
                AnyRadiance.Instance.GameObjects["Plat Sets"].Child("Hazard Plat/Hazard Respawn Marker").transform
                    .position, true);
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
        }

        private void GetChildren()
        {
            _bossCtrl = transform.parent.gameObject;

            AnyRadiance.Instance.GameObjects["CamLock A1"] = _bossCtrl.Child("CamLocks/CamLock A1");
            AnyRadiance.Instance.GameObjects["CamLock A2"] = _bossCtrl.Child("CamLocks/CamLock A2");
            AnyRadiance.Instance.GameObjects["CamLock Main"] = _bossCtrl.Child("CamLocks/CamLock Main");
            AnyRadiance.Instance.GameObjects["Plat Sets"] = _bossCtrl.Child("Plat Sets");
            AnyRadiance.Instance.GameObjects["Abyss Pit"] = _bossCtrl.Child("Abyss Pit");
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

            GameObject a2Plats = AnyRadiance.Instance.GameObjects["Plat Sets"].Child("P2 SetA");
            var rotator = a2Plats.AddComponent<PlatRotator>();
            foreach (Transform plat in a2Plats.transform)
            {
                if (!plat.gameObject.LocateMyFSM("radiant_plat")) continue;
                var platBody = plat.gameObject.AddComponent<Rigidbody2D>();
                platBody.isKinematic = true;
                platBody.interpolation = RigidbodyInterpolation2D.Interpolate;
                platBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                rotator.AddPlatform(plat.gameObject);
            }
            
            foreach (Transform plat in AnyRadiance.Instance.GameObjects["Plat Sets"].Child("Hazard Plat").transform)
            {
                if (!plat.gameObject.LocateMyFSM("radiant_plat")) continue;
                var platBody = plat.gameObject.AddComponent<Rigidbody2D>();
                platBody.isKinematic = true;
                platBody.interpolation = RigidbodyInterpolation2D.Interpolate;
                platBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                rotator.AddPlatform(plat.gameObject);
            }

            foreach (var plat in AnyRadiance.Instance.GameObjects["Plat Sets"].GetComponentsInChildren<PlayMakerFSM>())
            {
                if (plat.FsmName != "radiant_plat") continue;
                //var platSticker = new GameObject("Sticker");
                //platSticker.transform.SetParent(plat.transform, false);
                //platSticker.AddComponent<MovingPlatform>();
                plat.gameObject.AddComponent<MovingPlatform>();
            }
        }

#region Helpers
        private IEnumerator AnimPlayUntilFinished(string animName)
        {
            _animator.Play(animName);
            yield return new WaitUntil(() => _animator.IsFinished());
        }

        private void CamShake(string eventName)
        {
            GameCameras.instance.cameraShakeFSM.SendEvent(eventName);
        }

        public static void PlayOneShot(AudioClip clip, Vector3 location)
        {
            GameObject audioPlayer = AnyRadiance.Instance.GameObjects["Audio Player"].Spawn(location);
            var audioSource = audioPlayer.GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
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
            yield return AnimPlayUntilFinished("Tele In");
        }

        private IEnumerator TeleOut()
        {
            _collider.enabled = false;
            AnyRadiance.Instance.GameObjects["Legs"].SetActive(false);
            _rigidbody.velocity = Vector2.zero;
            AnyRadiance.Instance.Particles["Tele Out"].Play();
            PlayOneShot(AnyRadiance.Instance.AudioClips["Tele"], transform.position);
            yield return AnimPlayUntilFinished("Tele Out");
            _renderer.enabled = false;
        }

#endregion

        private IEnumerator StartBattle()
        {
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
                yield return AnimPlayUntilFinished("Cast");
                yield return ChooseAttack();

                yield return TeleOut();
                yield return TeleIn(new Vector3(Random.Range(ArenaInfo.A1Left, ArenaInfo.A1Right), transform.position.y, transform.position.z));
            }
        }

        private IEnumerator ChooseAttack()
        {
            var attackOptions = new[] { LaserColumns(), NailWalls() };
            var extraAttacks = new[] { NailBarrage(), SummonMegaOrb(), SummonBeamOrb() };
            var chosenAttack = attackOptions[Random.Range(0, attackOptions.Length)];
            if (_healthManager.hp <= Phase1HP / 2)
            {
                // Perform another attack simultaneously
                var extraAttack = extraAttacks[Random.Range(0, extraAttacks.Length)];
                StartCoroutine(extraAttack);
            }
            yield return chosenAttack;
        }
    }
}