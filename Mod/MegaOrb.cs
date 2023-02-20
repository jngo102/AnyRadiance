using Modding.Utils;
using System.Collections;
using UnityEngine;
using Vasi;

namespace AnyRadiance
{
    internal class MegaOrb : MonoBehaviour
    {
        private float _followTime = 2.0f / 3;
        private float _minScale = 0.5f;
        private float _trackingAcceleration = 1.5f;
        private float _maxTrackingSpeed = 12;
        private float _scale = 2;
        private float _maxSpeedIncrease = 2;
        private float _trackAccelIncrease = 1.5f;
        private float _scaleDivider = 2;

        private tk2dSpriteAnimator _animator;
        private AudioSource _audio;
        private CircleCollider2D _collider;
        private ParticleSystem _particles;
        private Rigidbody2D _rigidbody;

        private GameObject _appearGlow;

        public bool Following { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<tk2dSpriteAnimator>();
            _audio = GetComponent<AudioSource>();
            _collider = GetComponent<CircleCollider2D>();
            _particles = gameObject.Child("Particle System").GetComponent<ParticleSystem>();
            _rigidbody = GetComponent<Rigidbody2D>();

            _appearGlow = gameObject.Child("Appear Glow");

            foreach (var fsm in GetComponents<PlayMakerFSM>())
            {
                Destroy(fsm);
            }

            _collider.enabled = false;
        }

        private IEnumerator Start()
        {
            while (transform.localScale.x < _scale)
            {
                yield return null;
                transform.localScale += Vector3.one * Time.deltaTime;
            }

            Launch();
        }
        
        private void FixedUpdate()
        {
            if (!Following) return;
            Track(HeroController.instance.gameObject);
        }

        public void Launch()
        {
            _appearGlow.SetActive(true);
            Following = true;
            _rigidbody.velocity = new Vector2(Random.Range(-_maxTrackingSpeed, _maxTrackingSpeed), Random.Range(-_maxTrackingSpeed, _maxTrackingSpeed));
            StartCoroutine(Split(_followTime));
        }

        public void SetParams(float followTime, float minScale, float trackingAcceleration, float maxTrackingSpeed, float scale, float maxSpeedIncrease, float trackAccelIncrease, float scaleDivider)
        {
            _followTime = followTime;
            _minScale = minScale;
            _trackingAcceleration = trackingAcceleration;
            _maxTrackingSpeed = maxTrackingSpeed;
            _scale = scale;
            _maxSpeedIncrease = maxSpeedIncrease;
            _trackAccelIncrease = trackAccelIncrease;
            _scaleDivider = scaleDivider;
        }
        
        private IEnumerator Split(float delay)
        {
            yield return new WaitForSeconds(delay);
            _animator.Play("Orb End");
            _rigidbody.velocity = Vector2.zero;
            _audio.Stop();
            Following = false;
            _particles.Stop();
            if (_scale <= _minScale)
            {
                yield return new WaitUntil(() => _animator.IsFinished());
                Destroy(gameObject);
                yield break;
            }

            for (int i = 0; i < 2; i++)
            {
                var smallerOrb = gameObject.Spawn();
                smallerOrb.transform.SetPosition2D(transform.position);
                var orbComponent = smallerOrb.GetOrAddComponent<MegaOrb>();
                smallerOrb.transform.localScale = Vector3.one * _scale / 2;
                orbComponent.SetParams(_followTime, _minScale, _trackingAcceleration * _trackAccelIncrease, _maxTrackingSpeed + _maxSpeedIncrease, _scale / _scaleDivider, _trackAccelIncrease, _maxSpeedIncrease, _scaleDivider);
                orbComponent.Launch();
            }

            AnyRadiance.Instance.AudioClips["Projectile"].PlayOneShot(transform.position);
            Destroy(gameObject);
        }

        public void Track(GameObject target)
        {
            _rigidbody.velocity += (Vector2)(target.transform.position - transform.position).normalized * _trackingAcceleration;
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxTrackingSpeed);
        }
    }
}