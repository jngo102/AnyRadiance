using System.Collections.Generic;
using UnityEngine;

namespace AnyRadiance.Radiance
{
    internal class PlatformRotator : MonoBehaviour
    {
        private const float Radius = 8;
        private const float RotateSpeed = 45;
        private List<GameObject> _platforms = new();
        private float _refAngle;
        private Vector2 _arenaCenter;

        private void Awake()
        {
            _arenaCenter = new Vector2(60, 42);

            On.GameManager.HazardRespawn += OnRespawn;
        }
        private void Update()
        {
            if (_platforms.Count <= 0) return;

            for (int i = 0; i < _platforms.Count; i++)
            {
                GameObject plat = _platforms[i];
                var platform = plat.GetComponent<MovingPlatform>();
                if (platform == null) continue;
                platform.SetVelocity(new Vector2(
                    Radius * Mathf.Cos((_refAngle + 90 + i * 360f / _platforms.Count) * Mathf.Deg2Rad),
                    Radius * Mathf.Sin((_refAngle + 90 + i * 360f / _platforms.Count) * Mathf.Deg2Rad)));
            }

            _refAngle += RotateSpeed * Time.deltaTime;
        }

        private void OnDestroy()
        {
            On.GameManager.HazardRespawn -= OnRespawn;
        }

        private void OnRespawn(On.GameManager.orig_HazardRespawn orig, GameManager gameManager)
        {
            var minY = float.MaxValue;
            GameObject respawnPlat = null;
            foreach (var plat in _platforms)
            {
                if (plat.transform.position.y < minY)
                {
                    minY = plat.transform.position.y;
                    respawnPlat = plat;
                }
            }

            if (respawnPlat != null)
            {
                PlayerData.instance.SetHazardRespawn(
                    respawnPlat.transform.position + Vector3.up * 1.5f +
                    Vector3.right, true);
            }

            orig(gameManager);
        }

        /// <summary>
        /// Add a platform to the rotating ring of platforms.
        /// </summary>
        /// <param name="platform">The platform game object to add.</param>
        public void AddPlatform(GameObject platform)
        {
            _platforms.Add(platform);
            for (int i = 0; i < _platforms.Count; i++)
            {
                GameObject plat = _platforms[i];
                plat.transform.position = new Vector2(
                    _arenaCenter.x + Radius * Mathf.Cos(i * 360f / _platforms.Count * Mathf.Deg2Rad),
                    _arenaCenter.y + Radius * Mathf.Sin(i * 360f / _platforms.Count * Mathf.Deg2Rad));
            }
        }

        /// <summary>
        /// Undo respawning at the lowest platform.
        /// </summary>
        public void RemoveHazardPoint()
        {
            On.GameManager.HazardRespawn -= OnRespawn;
        }
    }
}
