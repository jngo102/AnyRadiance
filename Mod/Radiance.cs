using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Linq;
using System.Reflection;
using GlobalEnums;
using Modding;
using Modding.Utils;
using UnityEngine;
using Vasi;

namespace AnyRadiance
{
    internal partial class Radiance : MonoBehaviour
    {
        #region Constants
        private const int Phase1HP = 2000;
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
        private PlayMakerFSM _haloFSM;
        private HeroController _hc;
        private PlayerData _pd;
        private ParticleSystem _ptFeatherBurst;
        private ParticleSystem _ptTeleOut;
        private PlayMakerFSM _spellCtrl;
        #endregion

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
            AnyRadiance.Instance.AudioClips["Ghost"] = (AudioClip)cmdFSM.GetAction<AudioPlayerOneShotSingle>("Orb Summon").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Projectile"] = (AudioClip)cmdFSM.GetAction<AudioPlaySimple>("Spawn Fireball").oneShotClip.Value;
            AnyRadiance.Instance.AudioClips["Sword Create"] = (AudioClip)cmdFSM.GetAction<AudioPlayerOneShotSingle>("Dir").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Sword Shoot"] = (AudioClip)cmdFSM.GetAction<AudioPlayerOneShotSingle>("CW Fire").audioClip.Value;
            AnyRadiance.Instance.AudioClips["Tele"] = (AudioClip)teleFSM.GetAction<AudioPlayerOneShotSingle>("Antic").audioClip.Value;

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
            PlayOneShot(AnyRadiance.Instance.AudioClips["Appear"], transform.position);
            GameCameras.instance.cameraShakeFSM.SendEvent("BigShake");
            yield return StartCoroutine(TeleOut());
            yield return StartCoroutine(TeleIn(new Vector3(60.63f, 27, 0.006f)));
            yield return StartCoroutine(StartBattle());
        }

        private void OnDestroy()
        {
            var quake1Down = _spellCtrl.GetAction<CallMethodProper>("Quake1 Down");
            var quake2Down = _spellCtrl.GetAction<CallMethodProper>("Quake2 Down");
            ReflectionHelper.SetField<FsmStateAction, bool>(quake1Down, "enabled", true);
            ReflectionHelper.SetField<FsmStateAction, bool>(quake2Down, "enabled", true);

            var knight = HeroController.instance.gameObject.GetComponent<Knight>();
            if (knight != null) Destroy(knight);
        }

        private void GetChildren()
        {
            AnyRadiance.Instance.GameObjects["Glow"] = gameObject.Child("Eye Beam Glow");
            AnyRadiance.Instance.GameObjects["Beam"] = gameObject.Child("Eye Beam Glow/Ascend Beam");
            AnyRadiance.Instance.GameObjects["Eye Beam"] = gameObject.Child("Eye Beam Glow/Burst 1/Radiant Beam");
            _haloFSM = gameObject.Child("Halo").LocateMyFSM("Fader");
            AnyRadiance.Instance.GameObjects["Legs"] = gameObject.Child("Legs");
            _ptFeatherBurst = gameObject.Child("Pt Feather Burst").GetComponent<ParticleSystem>();
            _ptTeleOut = gameObject.Child("Pt Tele Out").GetComponent<ParticleSystem>();
            AnyRadiance.Instance.GameObjects["Shot Charge"] = gameObject.Child("Shot Charge");
            AnyRadiance.Instance.GameObjects["Tele Flash"] = gameObject.Child("Tele Flash");

            AnyRadiance.Instance.GameObjects["Beam"].CreatePool(30);
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
            _ptFeatherBurst.Play();
            CamShake("EnemyKillShake");
            _haloFSM.SendEvent("UP");
            yield return AnimPlayUntilFinished("Tele In");
        }

        private IEnumerator TeleOut()
        {
            _collider.enabled = false;
            AnyRadiance.Instance.GameObjects["Legs"].SetActive(false);
            _rigidbody.velocity = Vector2.zero;
            _ptTeleOut.Play();
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
                    fsm.ChangeTransition("Floor Antic", "SPIKES DOWN", "Up");
                    fsm.ChangeTransition("Floor Antic", "DOWN", "Up");
                    fsm.ChangeTransition("Spike Up", "SPIKES DOWN", "Up");
                    fsm.ChangeTransition("Spike Up", "DOWN", "Up");
                    fsm.ChangeTransition("Down", "FINISHED", "Up");
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