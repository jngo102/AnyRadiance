using System.Collections;
using GlobalEnums;
using System.Collections.Generic;
using UnityEngine;

namespace AnyRadiance.Radiance
{
    internal class Portal : MonoBehaviour
    {
        private enum PortalState
        {
            Open,
            Closed,
        }

        private PortalState _portalState = PortalState.Closed;

        public delegate void EnteredPortalEventHandler(Portal sender, GameObject teleported);

        public event EnteredPortalEventHandler OnEnterPortal;

        private Animator _animator;

        private static readonly List<int> _collisionLayers = new()
        {
            (int)PhysLayers.PLAYER,
            (int)PhysLayers.ENEMIES,
            (int)PhysLayers.HERO_ATTACK,
            (int)PhysLayers.ENEMY_ATTACK,
        };

        private MeshRenderer _renderer;

        private List<GameObject> _enteredFromOtherPortal = new();

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _renderer = GetComponentInChildren<MeshRenderer>();
        }

        private void OnEnable()
        {
            StartCoroutine(Open());
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!_collisionLayers.Contains(other.gameObject.layer)) return;
            if (EnteredFromOtherPortal(other.gameObject)) return;

            if (IsEnclosing(other.gameObject))
            {
                AnyRadiance.Instance.LogDebug($"{other.name} has entered {name}");
                OnEnterPortal?.Invoke(this, other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _enteredFromOtherPortal.Remove(other.gameObject);
        }

        /// <summary>
        /// Check whether the portal is enclosing a game object.
        /// </summary>
        /// <param name="gameObject">The game object to check whether it is enclosed.</param>
        /// <returns>Whether the game object is enclosed in the portal.</returns>
        private bool IsEnclosing(GameObject gameObject)
        {
            var otherBounds = gameObject.GetComponent<Collider2D>().bounds;
            return _renderer.bounds.min.x < otherBounds.min.x && _renderer.bounds.min.y < otherBounds.min.y &&
                   _renderer.bounds.max.x > otherBounds.max.x && _renderer.bounds.max.y > otherBounds.max.y;
        }

        /// <summary>
        /// Whether the game object has entered from the other portal.
        /// </summary>
        /// <param name="gameObject">The game object that is being checked.</param>
        /// <returns>Whether the game object entered from the other portal.</returns>
        private bool EnteredFromOtherPortal(GameObject gameObject)
        {
            return _enteredFromOtherPortal.Contains(gameObject);
        }

        /// <summary>
        /// Register that the game object has entered from the other portal.
        /// </summary>
        /// <param name="gameObject">The game object that is being checked.</param>
        public void SetEnteredFromOtherPortal(GameObject gameObject)
        {
            if (!_enteredFromOtherPortal.Contains(gameObject))
            {
                _enteredFromOtherPortal.Add(gameObject);
            }
        }

        /// <summary>
        /// Open the portal.
        /// </summary>
        public IEnumerator Open()
        {
            if (_portalState == PortalState.Open) yield break;
            _animator.Play("Open");
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
            _portalState = PortalState.Open;
        }

        /// <summary>
        /// Close the portal.
        /// </summary>
        public IEnumerator Close()
        {
            if (_portalState == PortalState.Closed) yield break;
            _animator.Play("Close");
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
            _portalState = PortalState.Closed;
        }
    }
}
