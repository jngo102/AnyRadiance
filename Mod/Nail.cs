using System.Collections;
using UnityEngine;

namespace AnyRadiance
{
    internal class Nail: MonoBehaviour
    {
        private tk2dSpriteAnimator _animator;
        private PolygonCollider2D _collider;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _animator = GetComponent<tk2dSpriteAnimator>();
            _collider = GetComponent<PolygonCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            StartCoroutine(EnableRoutine());
        }

        private void OnDisable()
        {
            _collider.enabled = false;
        }

        private IEnumerator EnableRoutine()
        {
            _animator.Play("Nail Antic");
            yield return new WaitUntil(() => _animator.CurrentFrame == 7);
            _collider.enabled = true;
        }
    }
}