using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vasi;
using Random = UnityEngine.Random;

namespace AnyRadiance.Radiance
{
    internal class BeamOrb : MonoBehaviour
    {
        private float _spinSpeed = 30;
        private float _trackingAcceleration = 0.3f;
        private float _maxTrackingSpeed = 3;
        private int _numBeams = 6;
        private float _beamInterval = 0.5f;
        private float _duration = 2;
        
        private int _spinDirection;

        private tk2dSpriteAnimator _animator;
        private Rigidbody2D _rigidbody;

        private GameObject _appearGlow;

        private List<GameObject> _beams = new();
        
        private Coroutine _intervalRoutine;

        public bool Tracking;

        private void Awake()
        {
            _animator = GetComponent<tk2dSpriteAnimator>();
            _rigidbody = GetComponent<Rigidbody2D>();

            _animator.enabled = false;

            _appearGlow = gameObject.Child("Appear Glow");
            Destroy(transform.Find("Hero Hurter").gameObject);

            gameObject.GetComponent<CircleCollider2D>().radius = 0.5f;
            gameObject.AddComponent<DamageHero>().damageDealt = 2;

            _spinDirection = new[] { -1, 1 }[Random.Range(0, 2)];

            foreach (var fsm in GetComponents<PlayMakerFSM>())
            {
                Destroy(fsm);
            }

            var glow = Instantiate(AnyRadiance.Instance.GameObjects["Glow"], transform);
            glow.SetActive(true);
            glow.transform.localScale *= 0.5f;
            glow.transform.localPosition += Vector3.down * 2.5f;
            glow.Child("Sprite").GetComponent<SpriteRenderer>().sortingOrder = 100;
            for (int i = 0; i < _numBeams; i++)
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

            if (_intervalRoutine != null) StopCoroutine(_intervalRoutine);
            _intervalRoutine = StartCoroutine(BeamInterval(_beamInterval));
            StartCoroutine(Dissipate(_duration));
        }

        private void Update()
        {
            transform.Rotate(0, 0, _spinDirection * _spinSpeed * Time.deltaTime);
            Track(HeroController.instance.gameObject);
        }

        public void SetParams(bool tracking, float spinSpeed, float trackingAcceleration, float maxTrackingSpeed, int numBeams, float beamInterval, float duration)
        {
            Tracking = tracking;
            _spinSpeed = spinSpeed;
            _trackingAcceleration = trackingAcceleration;
            _maxTrackingSpeed = maxTrackingSpeed;
            _numBeams = numBeams;
            _beamInterval = beamInterval;
            _duration = duration;

            foreach (var beam in _beams)
            {
                beam.Recycle();
            }

            _beams.Clear();

            for (int i = 0; i < _numBeams; i++)
            {
                GameObject beam = AnyRadiance.Instance.GameObjects["Eye Beam"].Spawn(transform);
                beam.name = "Beam Orb Beam " + i;
                beam.transform.SetRotationZ(i * 360.0f / _numBeams);
                beam.GetComponent<MeshRenderer>().sortingOrder = -100;
                _beams.Add(beam);
            }

            if (_intervalRoutine != null) StopCoroutine(_intervalRoutine);
            _intervalRoutine = StartCoroutine(BeamInterval(_beamInterval));
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

        public IEnumerator Dissipate(float delay)
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
            if (!Tracking) return;
            _rigidbody.velocity += (Vector2)(target.transform.position - transform.position).normalized * _trackingAcceleration;
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxTrackingSpeed);
        }
    }
}
