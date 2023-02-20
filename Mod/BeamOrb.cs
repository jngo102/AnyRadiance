using Modding.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using GlobalEnums;
using UnityEngine;
using Vasi;
using Random = UnityEngine.Random;

namespace AnyRadiance
{
    internal class BeamOrb : MonoBehaviour, IHitResponder
    {
        private bool _bouncer = false;
        private float _bounceForce = 2500;
        private float _spinSpeed = 30;
        private float _trackingAcceleration = 0.3f;
        private float _maxTrackingSpeed = 3;
        private int _numBeams = 6;
        private float _beamInterval = 0.5f;
        private float _duration = 2;

        private bool _bouncing;
        private int _spinDirection;

        private tk2dSpriteAnimator _animator;
        private Rigidbody2D _rigidbody;

        private GameObject _appearGlow;

        private List<GameObject> _beams = new();

        private Coroutine _intervalRoutine;

        private void Awake()
        {
            _animator = GetComponent<tk2dSpriteAnimator>();
            _rigidbody = GetComponent<Rigidbody2D>();

            _animator.enabled = false;

            _appearGlow = gameObject.Child("Appear Glow");

            _spinDirection = new[] {-1, 1}[Random.Range(0, 2)];

            foreach (var fsm in GetComponents<PlayMakerFSM>())
            {
                Destroy(fsm);
            }

            var glow = Instantiate(AnyRadiance.Instance.GameObjects["Glow"], transform);
            glow.SetActive(true);
            glow.transform.localScale *= 0.5f;
            glow.transform.localPosition += Vector3.down * 2.5f;
            glow.Child("Sprite").GetComponent<SpriteRenderer>().sortingOrder = 100;
            for (int i = 0; i <= _numBeams; i++)
            {
                GameObject beam = AnyRadiance.Instance.GameObjects["Eye Beam"].Spawn(transform);
                beam.transform.SetRotationZ(i * 360.0f / _numBeams);
                beam.GetComponent<MeshRenderer>().sortingOrder = -100;
                _beams.Add(beam);
            }
        }

        private IEnumerator Start()
        {
            transform.localScale = Vector3.one * 0.1f;

            _appearGlow.SetActive(true);

            while (transform.localScale.x < 1)
            {
                transform.localScale *= 1.05f;
                yield return null;
            }

            foreach (var beam in _beams)
            {
                beam.LocateMyFSM("Control").SendEvent("ANTIC");
            }

            _intervalRoutine = StartCoroutine(BeamInterval(_beamInterval));
            StartCoroutine(Dissipate(_duration));
        }

        private void Update()
        {
            transform.Rotate(0, 0, _spinDirection * _spinSpeed * Time.deltaTime);
            Track(HeroController.instance.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_bouncing) return;

            if (other.name == "Absolute Radiance")
            {
                foreach (var beam in _beams)
                {
                    if (_intervalRoutine != null) StopCoroutine(_intervalRoutine);
                    beam.LocateMyFSM("Control").SendEvent("FIRE");
                }

                float attackDirection = 0;
                if (Mathf.Abs(_rigidbody.velocity.x) > Mathf.Abs(_rigidbody.velocity.y))
                {
                    attackDirection = _rigidbody.velocity.x > 0 ? 0 : 180;
                }
                else
                {
                    attackDirection = _rigidbody.velocity.y > 0 ? 90 : 270;
                }
                
                var takeDamage = typeof(HealthManager).GetMethod("TakeDamage", BindingFlags.NonPublic | BindingFlags.Instance);
                takeDamage?.Invoke(other.GetComponent<HealthManager>(), new[] { new HitInstance
                {
                    AttackType = AttackTypes.Generic,
                    DamageDealt = 50,
                    Direction = attackDirection,
                    IgnoreInvulnerable = true,
                    Multiplier = 1,
                    Source = gameObject,
                } as object });
            }
        }


        public void Hit(HitInstance damageInstance)
        {
            if (!_bouncer || damageInstance.IsExtraDamage) return;

            var direction = Vector2.right;
            switch (damageInstance.Direction)
            {
                case 90:
                    direction = Vector2.up;
                    break;
                case 180:
                    direction = Vector2.left;
                    break;
                case 270:
                    direction = Vector2.down;
                    break;
            }
            
            _rigidbody.velocity += direction * _bounceForce;
            StartCoroutine(StartBounce());
        }

        private IEnumerator StartBounce()
        {
            _bouncing = true;
            gameObject.layer = (int)PhysLayers.HERO_ATTACK;
            yield return new WaitWhile(() => _rigidbody.velocity.magnitude >= 1);
            _bouncing = false;
            gameObject.layer = (int)PhysLayers.ENEMIES;
            if (_intervalRoutine != null) StopCoroutine(_intervalRoutine);
            _intervalRoutine = StartCoroutine(BeamInterval(_beamInterval));
        }

        public void SetParams(bool bouncer, float bounceForce, float spinSpeed, float trackingAcceleration, float maxTrackingSpeed, int numBeams, float beamInterval, float duration)
        {
            _bouncer = bouncer;
            _bounceForce = bounceForce;
            _spinSpeed = spinSpeed;
            _trackingAcceleration = trackingAcceleration;
            _maxTrackingSpeed = maxTrackingSpeed;
            _numBeams = numBeams;
            _beamInterval = beamInterval;
            _duration = duration;

            foreach (var beam in _beams)
            {
                Destroy(beam);
            }

            _beams.Clear();
            
            for (int i = 0; i <= _numBeams; i++)
            {
                GameObject beam = AnyRadiance.Instance.GameObjects["Eye Beam"].Spawn(transform);
                beam.transform.SetRotationZ(i * 360.0f / _numBeams);
                beam.GetComponent<MeshRenderer>().sortingOrder = -100;
                _beams.Add(beam);
            }
        }

        private IEnumerator BeamInterval(float interval)
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);
                
                foreach (var beam in _beams)
                {
                    PlayMakerFSM beamCtrl = beam.LocateMyFSM("Control");
                    if (beamCtrl.ActiveStateName == "Fire")
                    {
                        beamCtrl.SetState("Antic");
                        beam.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    else
                    {
                        beamCtrl.SetState("Fire");
                    }
                }

                if (_beams[0].LocateMyFSM("Control").ActiveStateName == "Fire")
                {
                    AnyRadiance.Instance.AudioClips["Beam Burst"].PlayOneShot(transform.position);
                }
            }
        }

        private IEnumerator Dissipate(float delay)
        {
            yield return new WaitForSeconds(delay);

            foreach (var beam in _beams)
            {
                beam.SetActive(false);
            }

            while (transform.localScale.magnitude >= 0.2f)
            {
                transform.localScale *= 0.96f;
                yield return null;
            }

            Destroy(gameObject);
        }

        public void Track(GameObject target)
        {
            _rigidbody.velocity += (Vector2)(target.transform.position - transform.position).normalized * _trackingAcceleration;
            if (_bouncing)
            {
                _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _bounceForce);
            }
            else
            {
                _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxTrackingSpeed);
            }
        }
    }
}
