using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vasi;
using Random = UnityEngine.Random;

namespace AnyRadiance
{
    internal class BeamOrb : MonoBehaviour
    {
        private const float SpinSpeed = 60;
        private const float TrackingAcceleration = 0.6f;
        private const float MaxTrackingSpeed = 6;

        private int _spinDirection;

        private tk2dSpriteAnimator _animator;
        private Rigidbody2D _rigidbody;

        private GameObject _appearGlow;

        private List<GameObject> _beams = new();

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
            for (int i = 0; i <= 6; i++)
            {
                GameObject beam = AnyRadiance.Instance.GameObjects["Eye Beam"].Spawn(transform);
                beam.transform.SetRotationZ(i * 60);
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

            StartCoroutine(BeamInterval(2.0f / 3));
            StartCoroutine(Dissipate(2));
        }

        private void Update()
        {
            transform.Rotate(0, 0, _spinDirection * SpinSpeed * Time.deltaTime);
            Track(HeroController.instance.gameObject);
        }

        private IEnumerator BeamInterval(float interval)
        {
            while (true)
            {
                foreach (var beam in _beams)
                {
                    PlayMakerFSM beamCtrl = beam.LocateMyFSM("Control");
                    beamCtrl.SetState(beamCtrl.ActiveStateName == "Antic" ? "Fire" : "Antic");
                }
                if (_beams[0].LocateMyFSM("Control").ActiveStateName == "Fire") Radiance.PlayOneShot(AnyRadiance.Instance.AudioClips["Beam Burst"], transform.position);

                yield return new WaitForSeconds(interval);
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
            _rigidbody.velocity += (Vector2)(target.transform.position - transform.position).normalized * TrackingAcceleration;
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, MaxTrackingSpeed);
        }
    }
}
