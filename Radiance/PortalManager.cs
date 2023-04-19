using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Vasi;

namespace AnyRadiance.Radiance
{
    internal class PortalManager : MonoBehaviour
    {
        private Portal _portal1;
        private Portal _portal2;

        private const float MinPortalDistance = 5;

        private const float MinTimeBetweenPortalPositionRandomization = 10000;
        private const float MaxTimeBetweenPortalPositionRandomization = 12000;
        private Timer _randomizePortalPositionsTimer;

        private List<GameObject> _doNotTeleport;

        private void Awake()
        {
            _randomizePortalPositionsTimer = new Timer(Random.Range(MinTimeBetweenPortalPositionRandomization,
                MaxTimeBetweenPortalPositionRandomization))
            {
                AutoReset = true
            };
            _randomizePortalPositionsTimer.Start();

            _doNotTeleport = new List<GameObject>();

            _portal1 = gameObject.Child("Portal 1").AddComponent<Portal>();
            _portal2 = gameObject.Child("Portal 2").AddComponent<Portal>();

            _portal1.OnEnterPortal += OnEnterPortal1;
            _portal2.OnEnterPortal += OnEnterPortal2;

            StartCoroutine(RandomizePortalPositions());
            _randomizePortalPositionsTimer.Elapsed += (_, _) =>
            {
                _randomizePortalPositionsTimer.Interval = Random.Range(MinTimeBetweenPortalPositionRandomization,
                    MaxTimeBetweenPortalPositionRandomization);
                StartCoroutine(RandomizePortalPositions());
            };
        }

        /// <summary>
        /// Triggered when a game object enters the first portal.
        /// </summary>
        /// <param name="portal">The first portal.</param>
        /// <param name="teleported">The game object that will be teleported.</param>
        private void OnEnterPortal1(Portal portal, GameObject teleported)
        {
            if (_doNotTeleport.Contains(teleported)) return;
            _portal2.SetEnteredFromOtherPortal(teleported);
            teleported.transform.root.position = _portal2.transform.position + teleported.transform.position -
                                                 _portal1.transform.position;
        }

        /// <summary>
        /// Triggered when a game object enters the second portal.
        /// </summary>
        /// <param name="portal">The second portal.</param>
        /// <param name="teleported">The game object that will be teleported.</param>
        private void OnEnterPortal2(Portal portal, GameObject teleported)
        {
            if (_doNotTeleport.Contains(teleported)) return;
            _portal1.SetEnteredFromOtherPortal(teleported);
            teleported.transform.root.position = _portal1.transform.position + teleported.transform.position -
                                                 _portal2.transform.position;
        }

        /// <summary>
        /// Set the positions of the two portals.
        /// </summary>
        /// <param name="portal1Position">The position of portal 1.</param>
        /// <param name="portal2Position">The position of portal 2.</param>
        public void SetPortalPositions(Vector2 portal1Position, Vector2 portal2Position)
        {
            _portal1.transform.SetPosition2D(portal1Position);
            _portal2.transform.SetPosition2D(portal2Position);
        }

        /// <summary>
        /// Randomize the two portals' positions.
        /// </summary>
        public IEnumerator RandomizePortalPositions()
        {
            StartCoroutine(_portal1.Close());
            yield return _portal2.Close();

            var pos1 = Vector2.zero;
            var pos2 = Vector2.zero;
            while (Vector2.Distance(pos1, pos2) < MinPortalDistance)
            {
                var x = Random.Range(ArenaInfo.CurrentLeft, ArenaInfo.CurrentRight);
                var y = Random.Range(ArenaInfo.CurrentBottom, ArenaInfo.CurrentTop);
                pos1 = new Vector2(x, y);
                x = Random.Range(ArenaInfo.CurrentLeft, ArenaInfo.CurrentRight);
                y = Random.Range(ArenaInfo.CurrentBottom, ArenaInfo.CurrentTop);
                pos2 = new Vector2(x, y);
            }

            SetPortalPositions(pos1, pos2);

            StartCoroutine(_portal1.Open());
            yield return _portal2.Open();
        }

        /// <summary>
        /// Add a game object to the list of game objects that should not be teleported.
        /// </summary>
        /// <param name="gameObject">The game object to exclude teleportation.</param>
        public void ExcludeTeleport(GameObject gameObject)
        {
            if (!_doNotTeleport.Contains(gameObject))
            {
                _doNotTeleport.Add(gameObject);
            }
        }
    }
}
