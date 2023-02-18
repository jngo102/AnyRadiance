using System.Collections.Generic;
using UnityEngine;

namespace AnyRadiance
{
    internal class PlatRotator : MonoBehaviour
    {
        private const float Radius = 8;
        private const float RotateSpeed = 45;
        private List<GameObject> _platforms = new();
        private float _refAngle;
        private Vector2 _arenaCenter;
        
        private void Awake()
        {
            _arenaCenter = new Vector2(60, 40);
        }

        private void Update()
        {
            if (_platforms.Count<= 0) return;

            for (int i = 0; i < _platforms.Count; i++)
            {
                GameObject plat = _platforms[i];
                plat.GetComponent<Rigidbody2D>().velocity= new Vector2(
                    Radius * Mathf.Cos((_refAngle + 90 + i * 360f / _platforms.Count) * Mathf.Deg2Rad),
                    Radius * Mathf.Sin((_refAngle + 90 + i * 360f / _platforms.Count) * Mathf.Deg2Rad));
            }

            _refAngle += RotateSpeed * Time.deltaTime;
        }

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
    }
}
