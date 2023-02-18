using Modding.Utils;
using System.Collections;
using UnityEngine;
using Vasi;

namespace AnyRadiance
{
    internal class MegaOrb : MonoBehaviour
    {
        private const float FollowTime = 2.0f / 3;
        private const float MinScale = 0.5f;

        public float TrackingAcceleration = 1.5f;
        public float MaxTrackingSpeed = 12;
        public float Scale = 2;

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
            while (transform.localScale.x < Scale)
            {
                yield return null;
                transform.localScale += Vector3.one * Time.deltaTime;
            }

            Launch();
        }

        public void Launch()
        {
            _appearGlow.SetActive(true);
            Following = true;
            _rigidbody.velocity = new Vector2(Random.Range(-MaxTrackingSpeed, MaxTrackingSpeed), Random.Range(-MaxTrackingSpeed, MaxTrackingSpeed));
            StartCoroutine(Split(FollowTime));
        }

        private void FixedUpdate()
        {
            if (!Following) return;
            Track(HeroController.instance.gameObject);
        }

        private IEnumerator Split(float delay)
        {
            yield return new WaitForSeconds(delay);
            _animator.Play("Orb End");
            _rigidbody.velocity = Vector2.zero;
            _audio.Stop();
            Following = false;
            _particles.Stop();
            if (Scale <= MinScale)
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
                smallerOrb.transform.localScale = Vector3.one * Scale / 2;
                orbComponent.Scale = Scale / 2;
                orbComponent.MaxTrackingSpeed = MaxTrackingSpeed + 2;
                orbComponent.TrackingAcceleration = TrackingAcceleration * 1.5f;
                orbComponent.Launch();
            }

            Radiance.PlayOneShot(AnyRadiance.Instance.AudioClips["Projectile"], transform.position);
            Destroy(gameObject);
        }

        public void Track(GameObject target)
        {
            _rigidbody.velocity += (Vector2)(target.transform.position - transform.position).normalized * TrackingAcceleration;
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, MaxTrackingSpeed);
        }
    }
}